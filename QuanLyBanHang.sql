create database QuanLyBanHang;
drop database QuanLyBanHang;
use QuanLyBanHang;

select * from "Order";
select * from OrderItem

CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Supplier (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(255) UNIQUE NOT NULL,
    ContactName NVARCHAR(255),
    Phone NVARCHAR(20) UNIQUE NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(255) UNIQUE NOT NULL,
    CostPrice DECIMAL(10, 2) NOT NULL,
    SellingPrice DECIMAL(10, 2) NOT NULL,
    "Description" NVARCHAR(MAX),
    ImageUrl NVARCHAR(255),
    CategoryID INT,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category (CategoryID)
);

CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT UNIQUE,
    Quantity INT DEFAULT 0,
    CONSTRAINT FK_Inventory_Product FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE Purchase (
    PurchaseID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT,
    SupplierID INT,
    PurchaseDate DATETIME DEFAULT GETDATE(),
    Quantity INT NOT NULL,
    CostPrice DECIMAL(10, 2),
    TotalCost DECIMAL(10, 2),
    CONSTRAINT FK_Purchase_Product FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    CONSTRAINT FK_Purchase_Supplier FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID)
);

-- Gop 2 bang Admin va Customer lai va chia Role
CREATE TABLE "User" (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(255) NOT NULL,
    Email NVARCHAR(50) UNIQUE NOT NULL,
    "Address" NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20) UNIQUE NOT NULL,
    UserName NVARCHAR(50) UNIQUE NOT NULL,
    "PassWord" NVARCHAR(50) NOT NULL,
	"Role" INT,
);

-- Khi thuc hien don hang thi gio hang phai duoc xoa va chuyen sang bang Order
CREATE TABLE Cart (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
	UserId INT,
	ProductID INT,
	Quantity INT,
	SellingPrice DECIMAL(10, 2), -- Gia ban cua san pham tham chieu den gia trong Product
	TotalPrice DECIMAL(10, 2), -- Tong gia cua san pham nay = SellingPrice * Quantity
	CONSTRAINT FK_Cart_Product FOREIGN KEY (ProductID) REFERENCES Product (ProductID),
	CONSTRAINT FK_Cart_User FOREIGN KEY (UserId) REFERENCES "User" (UserId)
);

CREATE TABLE "Order" (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    ShippingAddress NVARCHAR(255) NOT NULL,
    PaymentMethod NVARCHAR(255) NOT NULL,
	"Status" NVARCHAR(50) NOT NULL DEFAULT 'Processing',
    CONSTRAINT FK_Order_User FOREIGN KEY (UserId) REFERENCES "User" (UserId)
);

CREATE TABLE OrderItem (
	OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    Quantity INT NOT NULL,
    SellingPrice DECIMAL(10, 2) NOT NULL, -- Gia ban cua san pham tham chieu den gia trong Product
	TotalPrice DECIMAL(10, 2), -- Tong gia cua san pham nay = SellingPrice * Quantity
    CONSTRAINT FK_OrderItem_Order FOREIGN KEY (OrderId) REFERENCES "Order" (OrderId),
    CONSTRAINT FK_OrderItem_Product FOREIGN KEY (ProductId) REFERENCES Product (ProductId)
);

CREATE TABLE Review (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    ProductId INT,
    Rating INT NOT NULL,
    Comment NVARCHAR(MAX),
    CONSTRAINT FK_Review_User FOREIGN KEY (UserId) REFERENCES "User" (UserId),
    CONSTRAINT FK_Review_Product FOREIGN KEY (ProductId) REFERENCES Product (ProductId),
);

CREATE TABLE Promotion (
    PromotionId INT IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(255),
    "Description" NVARCHAR(MAX),
    StartDate DATE,
	EndDate DATE
);

-- Sau khi tạo 1 Product mới thì tự động tạo 1 Inventory tương ứng.
CREATE TRIGGER trg_CreateInventory
ON Product
AFTER INSERT
AS
BEGIN
    -- Insert into Inventory for each newly inserted Product
    INSERT INTO Inventory (ProductID)
    SELECT ProductID
    FROM inserted;
END;

-- Khi thực hiện Purchase có thể là Create, Update hoặc Delete thì Quantity trong Inventory tự động cập nhật.
CREATE TRIGGER trg_Purchase_UpdateInventory
ON Purchase
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Update Inventory Quantity after Purchase operation

    -- Check if the action is an INSERT or UPDATE
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Update Inventory for inserted or updated records
        UPDATE Inventory
        SET Quantity = Inventory.Quantity + i.Quantity
        FROM Inventory
        INNER JOIN inserted i ON Inventory.ProductID = i.ProductID;
    END

    -- Check if the action is a DELETE
    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        -- Update Inventory for deleted records
        UPDATE Inventory
        SET Quantity = Inventory.Quantity - d.Quantity
        FROM Inventory
        INNER JOIN deleted d ON Inventory.ProductID = d.ProductID;
    END
END;

-- Khi Create hoặc Update 1 Purchase thì trong Purchase có cột CostPrice sẽ tham chiếu tới CostPrice của Product và sẽ tự động tính TotalCost = Quantity * CostPrice.
CREATE TRIGGER trg_Purchase_UpdateCostAndTotalCost
ON Purchase
AFTER INSERT, UPDATE
AS
BEGIN
    -- Update CostPrice and TotalCost after Purchase operation

    -- Check if the action is an INSERT or UPDATE
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Update CostPrice for inserted or updated records
        UPDATE p
        SET CostPrice = pr.CostPrice,
            TotalCost = i.Quantity * pr.CostPrice
        FROM Purchase p
        INNER JOIN inserted i ON p.PurchaseID = i.PurchaseID
        INNER JOIN Product pr ON p.ProductID = pr.ProductID;
    END
END;

-- Khi Update lại Product nếu có sửa giá CostPrice thì trong trong Purchase cũng phải sửa CostPrice cho tương ứng.
CREATE TRIGGER trg_Product_UpdateCostPrice
ON Product
AFTER UPDATE
AS
BEGIN
    -- Update corresponding CostPrice in Purchase after Product update

    -- Check if CostPrice is updated
    IF UPDATE(CostPrice)
    BEGIN
        -- Update CostPrice in Purchase for corresponding Product
        UPDATE p
        SET CostPrice = pr.CostPrice,
            TotalCost = p.Quantity * pr.CostPrice
        FROM Purchase p
        INNER JOIN inserted i ON p.ProductID = i.ProductID
        INNER JOIN Product pr ON p.ProductID = pr.ProductID;
    END
END;

-- Update lại Quantity Inventory Khi có OrderItem mới đc thêm vào
CREATE TRIGGER trg_OrderItem_UpdateInventory
ON OrderItem
AFTER INSERT
AS
BEGIN
    -- Cập nhật số lượng tồn kho sau khi có đơn đặt hàng mới
    -- Lấy ra các sản phẩm và số lượng từ bảng OrderItem (vừa mới được thêm)
    UPDATE inv
    SET inv.Quantity = inv.Quantity - i.Quantity
    FROM Inventory inv
    INNER JOIN inserted i ON inv.ProductID = i.ProductID;
END;

-- Chỉ được xóa sản phẩm khi không có bất kỳ Purchase nào và xóa Inventory trước 
IF OBJECT_ID('DeleteProductWithCheck', 'P') IS NOT NULL
    DROP PROCEDURE DeleteProductWithCheck;
GO
CREATE PROCEDURE DeleteProductWithCheck
    @ProductID INT,
    @ErrorMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SET @ErrorMessage = NULL; -- Đặt giá trị mặc định cho tham số

    -- Kiểm tra xem sản phẩm có bất kỳ giao dịch mua hàng liên quan nào hay không
    IF NOT EXISTS (SELECT 1 FROM Purchase WHERE ProductID = @ProductID)
    BEGIN
        -- Không có giao dịch mua hàng liên quan, xóa bản ghi trong bảng Inventory trước
        DELETE FROM Inventory WHERE ProductID = @ProductID;

        -- Sau đó, xóa sản phẩm
        DELETE FROM Product WHERE ProductID = @ProductID;
    END
    ELSE
    BEGIN
        -- Sản phẩm có giao dịch mua hàng liên quan, đặt thông báo lỗi
        SET @ErrorMessage = 'Product cannot be deleted';
    END
END;
GO
