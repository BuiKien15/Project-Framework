create database QuanLyBanHang;
drop database QuanLyBanHang;
use QuanLyBanHang;

CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(255) NOT NULL
);
DBCC CHECKIDENT ('Category', RESEED, 0);
select * from Category
insert into Category(CategoryName) values (N'Giày thể thao');
insert into Category(CategoryName) values  (N'Găng tay thủ môn');
insert into Category(CategoryName) values (N'Áo đá banh');
insert into Category(CategoryName) values (N'Túi đựng đồ');
insert into Category(CategoryName) values (N'Bọc ống đồng');
insert into Category(CategoryName) values (N'Poster');
insert into Category(CategoryName) values (N'Vớ mang giày');
insert into Category(CategoryName) values (N'Cúp');
insert into Category(CategoryName) values (N'Bóng đá banh');
insert into Category(CategoryName) values (N'Vợt cầu lông');
delete from Category

CREATE TABLE Supplier (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(255) NOT NULL,
    ContactName NVARCHAR(255),
    Phone NVARCHAR(20),
    Email NVARCHAR(255)
);

CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(255) UNIQUE NOT NULL,
    CostPrice DECIMAL(10, 2) NOT NULL,
    SellingPrice DECIMAL(10, 2) NOT NULL,
    "Description" NVARCHAR(MAX),
    ImageUrl NVARCHAR(255),
    CategoryID INT,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);
delete from Product
INSERT INTO Product (ProductName, CostPrice, SellingPrice, "Description", ImageUrl, CategoryID)
VALUES
    ('Nike Mercurial Superfly', 150.00, 200.00, 'Premium soccer cleats with excellent traction and speed.', 'nike-mercurial.png',1),
    ('Adidas Predator 20.1', 120.00, 180.00, 'Control-focused cleats with enhanced ball grip.', 'adidas-predator.png',1),
    ('Puma Future 6.1', 100.00, 150.00, 'Innovative cleats for agility and quick movements on the field.', 'puma-future.png',1),
    ('Mizuno Morelia Neo II', 90.00, 130.00, 'Classic leather soccer cleats for superior touch and comfort.', 'mizuno-morelia.png',1),
    ('Under Armour Magnetico Pro', 110.00, 160.00, 'Responsive cleats designed for precision and control.', 'under-armour-magnetico.png',1),
    ('New Balance Furon V6', 95.00, 140.00, 'Lightweight cleats for explosive speed on the pitch.', 'new-balance-furon.png', 1),
    ('Puma One 20.1', 110.00, 160.00, 'Versatile cleats for all-around performance and control.', 'puma-one.png', 1),
    ('Adidas X Ghosted.1', 130.00, 190.00, 'Sleek cleats with a lightweight design for unmatched acceleration.', 'adidas-ghosted.png', 1);
	DELETE FROM Product WHERE CategoryID = 1;
-- Tạo 8 sản phẩm cho category_id = 2 (Găng tay)
INSERT INTO Product (ProductName, CostPrice, SellingPrice, "Description", ImageUrl, CategoryID)
VALUES
    (N'Găng tay Manuel Neuer', 10.00, 20.00, N'Găng tay thủ môn được lấy cảm hứng từ Manuel Neuer', 'manuel_neuer.png', 2),
    (N'Găng tay Alisson Becker', 15.00, 25.00, N'Găng tay thủ môn được lấy cảm hứng từ Alisson Becker', 'alisson_becker.png', 2),
    (N'Găng tay Jan Oblak', 12.50, 22.50, N'Găng tay thủ môn được lấy cảm hứng từ Jan Oblak', 'jan_oblak.png', 2),
    (N'Găng tay Keylor Navas', 18.75, 28.75, N'Găng tay thủ môn được lấy cảm hứng từ Keylor Navas', 'keylor_navas.png', 2),
    (N'Găng tay Ederson', 14.25, 24.25, N'Găng tay thủ môn được lấy cảm hứng từ Ederson', 'ederson.png', 2),
    (N'Găng tay Thibaut Courtois', 11.50, 21.50, N'Găng tay thủ môn được lấy cảm hứng từ Thibaut Courtois', 'thibaut_courtois.png', 2),
    (N'Găng tay Marc-André ter Stegen', 16.75, 26.75, N'Găng tay thủ môn được lấy cảm hứng từ Marc-André ter Stegen', 'marc_andre_ter_stegen.png', 2),
    (N'Găng tay Gianluigi Donnarumma', 13.25, 23.25, N'Găng tay thủ môn được lấy cảm hứng từ Gianluigi Donnarumma', 'gianluigi_donnarumma.png', 2);
	DELETE FROM Product WHERE CategoryID = 2;
-- Thêm dữ liệu áo đấu của các đội bóng hàng đầu châu Âu vào bảng Product
INSERT INTO Product (ProductName, CostPrice, SellingPrice, "Description", ImageUrl, CategoryID)
VALUES
    (N'Áo đấu Manchester City', 35.00, 70.00, N'Áo đấu chính thức của Manchester City Football Club - Ông vua châu Âu với cú ăn 5 vĩ đại', 'manchester_city.png', 3),
    (N'Áo đấu Liverpool', 25.00, 50.00, N'Áo đấu chính thức của Liverpool Football Club', 'liverpool.png', 3),
    (N'Áo đấu Manchester United', 27.50, 55.00, N'Áo đấu chính thức của Manchester United Football Club - Ăn mày quá khứ', 'manchester_united.png', 3),
    (N'Áo đấu Real Madrid', 30.00, 60.00, N'Áo đấu chính thức của Real Madrid Club de Fútbol - Vadrid ăn hên 4-0', 'real_madrid.png', 3),
    (N'Áo đấu Barcelona', 28.00, 56.00, N'Áo đấu chính thức của Futbol Club Barcelona - FC nợ lương', 'barcelona.png', 3),
    (N'Áo đấu Bayern Munich', 29.00, 58.00, N'Áo đấu chính thức của FC Bayern Munich', 'bayern_munich.png', 3),
    (N'Áo đấu Paris Saint-Germain', 26.00, 52.00, N'Áo đấu chính thức của Paris Saint-Germain Football Club', 'psg.png', 3),
    (N'Áo đấu Juventus', 29.50, 59.00, N'Áo đấu chính thức của Juventus Football Club', 'juventus.png', 3);
	DELETE FROM Product WHERE CategoryID = 3;

	-- Thêm dữ liệu túi của các đội bóng châu Âu vào bảng Product
INSERT INTO Product (ProductName, CostPrice, SellingPrice, "Description", ImageUrl, CategoryID)
VALUES
    (N'Túi Chelsea', 50.00, 100.00, N'Túi của Chelsea Football Club', 'chelsea_bag.png', 4),
    (N'Túi AC Milan', 45.00, 90.00, N'Túi của AC Milan', 'ac_milan_bag.png', 4),
    (N'Túi Borussia Dortmund', 47.50, 95.00, N'Túi của Borussia Dortmund', 'borussia_dortmund_bag.png', 4),
    (N'Túi Arsenal', 50.00, 100.00, N'Túi của Arsenal Football Club', 'arsenal_bag.png', 4),
    (N'Túi Tottenham Hotspur', 48.00, 96.00, N'Túi của Tottenham Hotspur Football Club', 'tottenham_hotspur_bag.png', 4),
    (N'Túi Inter Milan', 49.00, 98.00, N'Túi của Inter Milan', 'inter_milan_bag.png', 4),
    (N'Túi Paris Saint-Germain', 46.00, 92.00, N'Túi của Paris Saint-Germain Football Club', 'psg_bag.png', 4),
    (N'Túi AS Roma', 49.50, 99.00, N'Túi của AS Roma', 'as_roma_bag.png', 4);

	--Thêm bọc ống đồng--
INSERT INTO Product (ProductName, CostPrice, SellingPrice, "Description", ImageUrl, CategoryID)
VALUES
    (N'Bọc ống đồng 1/4 inch', 10.00, 20.00, N'Bọc ống đồng kích thước 1/4 inch', 'boc_ong_dong_1_4.png', 5),
    (N'Bọc ống đồng 3/8 inch', 12.00, 24.00, N'Bọc ống đồng kích thước 3/8 inch', 'boc_ong_dong_3_8.png', 5),
    (N'Bọc ống đồng 1/2 inch', 15.00, 30.00, N'Bọc ống đồng kích thước 1/2 inch', 'boc_ong_dong_1_2.png', 5),
    (N'Bọc ống đồng 5/8 inch', 18.00, 36.00, N'Bọc ống đồng kích thước 5/8 inch', 'boc_ong_dong_5_8.png', 5),
    (N'Bọc ống đồng 3/4 inch', 20.00, 40.00, N'Bọc ống đồng kích thước 3/4 inch', 'boc_ong_dong_3_4.png', 5),
    (N'Bọc ống đồng 7/8 inch', 22.00, 44.00, N'Bọc ống đồng kích thước 7/8 inch', 'boc_ong_dong_7_8.png', 5),
    (N'Bọc ống đồng 1 inch', 25.00, 50.00, N'Bọc ống đồng kích thước 1 inch', 'boc_ong_dong_1.png', 5),
    (N'Bọc ống đồng 1 1/8 inch', 28.00, 56.00, N'Bọc ống đồng kích thước 1 1/8 inch', 'boc_ong_dong_1_1_8.png', 5);
		DELETE FROM Product WHERE CategoryID = 5;

	--Thêm poster--
INSERT INTO Product (ProductName, CostPrice, SellingPrice, Description, ImageUrl, CategoryID)
VALUES
    (N'Poster Cristiano Ronaldo', 10.00, 20.00, N'Poster của Cristiano Ronaldo-Anh Liêm-SIUUUUUU', 'cristiano_ronaldo_poster.png', 6),
    (N'Poster Lionel Messi', 12.00, 24.00, N'Poster của Lionel Messi-Anh Mười Gió', 'lionel_messi_poster.png', 6),
    (N'Poster Neymar Jr.', 15.00, 30.00, N'Poster của Neymar Jr.', 'neymar_jr_poster.png', 6),
    (N'Poster Mohamed Salah', 18.00, 36.00, N'Poster của Mohamed Salah', 'mohamed_salah_poster.png', 6),
    (N'Poster Kylian Mbappé', 20.00, 40.00, N'Poster của Kylian Mbappé', 'kylian_mbappe_poster.png', 6),
    (N'Poster Robert Lewandowski', 22.00, 44.00, N'Poster của Robert Lewandowski', 'robert_lewandowski_poster.png', 6),
    (N'Poster Kevin De Bruyne', 25.00, 50.00, N'Poster của Kevin De Bruyne', 'kevin_de_bruyne_poster.png', 6),
    (N'Poster Rodri', 28.00, 56.00, N'Poster của Rodri', 'rodri_poster.png', 6);

	--Thêm tất mang giày--
	INSERT INTO Product (ProductName, CostPrice, SellingPrice, Description, ImageUrl, CategoryID)
VALUES
    (N'Tất thể thao Adidas', 10.00, 20.00, N'Tất thể thao Adidas chất liệu cao cấp', 'tat_the_thao_adidas.png', 7),
    (N'Tất chân ngắn Nike', 12.00, 24.00, N'Tất chân ngắn Nike tiện dụng và thoáng khí', 'tat_chan_ngan_nike.png', 7),
    (N'Tất đá bóng Puma', 15.00, 30.00, N'Tất đá bóng Puma chống trơn trượt', 'tat_da_bong_puma.png', 7),
    (N'Tất chạy Asics', 18.00, 36.00, N'Tất chạy Asics êm ái và bền bỉ', 'tat_chay_asics.png', 7),
    (N'Tất tập gym Reebok', 20.00, 40.00, N'Tất tập gym Reebok thoải mái và thấm hút mồ hôi', 'tat_tap_gym_reebok.png', 7),
    (N'Tất thể thao Under Armour', 22.00, 44.00, N'Tất thể thao Under Armour linh hoạt và nhẹ nhàng', 'tat_the_thao_under_armour.png', 7),
    (N'Tất chống trượt New Balance', 25.00, 50.00, N'Tất chống trượt New Balance an toàn và thoải mái', 'tat_chong_truot_new_balance.png', 7),
    (N'Tất thể thao Diadora', 28.00, 56.00, N'Tất thể thao Diadora phong cách và cá tính', 'tat_the_thao_diadora.png', 7);

	--Thêm cup--
	INSERT INTO Product (ProductName, CostPrice, SellingPrice, Description, ImageUrl, CategoryID)
VALUES
    (N'Cúp FIFA World Cup', 100000.00, 200000.00, N'Cúp FIFA World Cup là giải thưởng cao quý nhất trong bóng đá', 'cup_fifa_world_cup.png', 8),
    (N'Cúp UEFA Champions League', 80000.00, 160000.00, N'Cúp UEFA Champions League dành cho nhà vô địch câu lạc bộ châu Âu', 'cup_uefa_champions_league.png', 8),
    (N'Cúp Copa America', 75000.00, 150000.00, N'Cúp Copa America dành cho nhà vô địch bóng đá Nam Mỹ', 'cup_copa_america.png', 8),
    (N'Cúp European Championship', 70000.00, 140000.00, N'Cúp European Championship dành cho nhà vô địch châu Âu', 'cup_european_championship.png', 8),
    (N'Premier League Cup', 65000.00, 130000.00, N'Premier League Cup dành cho nhà vô địch giải đấu Premier League', 'cup_premier_league.png', 8),
    (N'Cúp FIFA Club World Cup', 60000.00, 120000.00, N'Cúp FIFA Club World Cup dành cho nhà vô địch thế giới câu lạc bộ', 'cup_fifa_club_world_cup.png', 8),
    (N'Siêu Cup Châu Âu', 55000.00, 110000.00, N'Siêu Cup Châu Âu dành cho nhà vô địch Siêu Cup Châu Âu', 'cup_super_cup.png', 8),
    (N'Siêu Cup Liên lục địa', 50000.00, 100000.00, N'Siêu Cup Liên lục địa dành cho nhà vô địch khu vực OFC', 'cup_ofc_nations_cup.png', 8);

	--Thêm bóng đá banh
	INSERT INTO Product (ProductName, CostPrice, SellingPrice, Description, ImageUrl, CategoryID)
VALUES
    (N'Trái bóng đá banh 1', 50000.00, 100000.00, N'Trái bóng đá banh số 1', 'ball_soccer_1.png', 9),
    (N'Trái bóng đá banh 2', 50000.00, 100000.00, N'Trái bóng đá banh số 2', 'ball_soccer_2.png', 9),
    (N'Trái bóng đá banh 3', 50000.00, 100000.00, N'Trái bóng đá banh số 3', 'ball_soccer_3.png', 9),
    (N'Trái bóng đá banh 4', 50000.00, 100000.00, N'Trái bóng đá banh số 4', 'ball_soccer_4.png', 9),
    (N'Trái bóng đá banh 5', 50000.00, 100000.00, N'Trái bóng đá banh số 5', 'ball_soccer_5.png', 9),
    (N'Trái bóng đá banh 6', 50000.00, 100000.00, N'Trái bóng đá banh số 6', 'ball_soccer_6.png', 9),
    (N'Trái bóng đá banh 7', 50000.00, 100000.00, N'Trái bóng đá banh số 7', 'ball_soccer_7.png', 9),
    (N'Trái bóng đá banh 8', 50000.00, 100000.00, N'Trái bóng đá banh số 8', 'ball_soccer_8.png', 9);
	--Thêm vợt cầu lông
	INSERT INTO Product (ProductName, CostPrice, SellingPrice, Description, ImageUrl, CategoryID)
VALUES
    (N'Vợt cầu lông 1', 80000.00, 160000.00, N'Vợt cầu lông số 1', 'badminton_racket_1.png', 10),
    (N'Vợt cầu lông 2', 90000.00, 170000.00, N'Vợt cầu lông số 2', 'badminton_racket_2.png', 10),
    (N'Vợt cầu lông 3', 85000.00, 165000.00, N'Vợt cầu lông số 3', 'badminton_racket_3.png', 10),
    (N'Vợt cầu lông 4', 95000.00, 175000.00, N'Vợt cầu lông số 4', 'badminton_racket_4.png', 10),
    (N'Vợt cầu lông 5', 88000.00, 168000.00, N'Vợt cầu lông số 5', 'badminton_racket_5.png', 10),
    (N'Vợt cầu lông 6', 92000.00, 172000.00, N'Vợt cầu lông số 6', 'badminton_racket_6.png', 10),
    (N'Vợt cầu lông 7', 87000.00, 167000.00, N'Vợt cầu lông số 7', 'badminton_racket_7.png', 10),
    (N'Vợt cầu lông 8', 89000.00, 169000.00, N'Vợt cầu lông số 8', 'badminton_racket_8.png', 10);
	select *from product
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT UNIQUE,
    Quantity INT DEFAULT 0,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE Purchase (
    PurchaseID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT,
    SupplierID INT,
    PurchaseDate DATETIME DEFAULT GETDATE(),
    Quantity INT NOT NULL,
    CostPrice DECIMAL(10, 2),
    TotalCost DECIMAL(10, 2),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID)
);

-- Gop 2 bang Admin va Customer lai va chia Role
CREATE TABLE "User" (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(255) NOT NULL,
    Email NVARCHAR(50) UNIQUE NOT NULL,
    "Address" NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
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
	FOREIGN KEY (ProductID) REFERENCES Product (ProductID),
	FOREIGN KEY (UserId) REFERENCES "User" (UserId)
);

CREATE TABLE "Order" (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    ShippingAddress NVARCHAR(255) NOT NULL,
    PaymentMethod NVARCHAR(255) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES "User" (UserId)
);

CREATE TABLE OrderItem (
	OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    Quantity INT NOT NULL,
    SellingPrice DECIMAL(10, 2) NOT NULL, -- Gia ban cua san pham tham chieu den gia trong Product
	TotalPrice DECIMAL(10, 2), -- Tong gia cua san pham nay = SellingPrice * Quantity
    FOREIGN KEY (OrderId) REFERENCES "Order" (OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product (ProductId)
);

CREATE TABLE Review (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    ProductId INT,
    Rating INT NOT NULL,
    Comment NVARCHAR(MAX),
    FOREIGN KEY (UserId) REFERENCES "User" (UserId),
    FOREIGN KEY (ProductId) REFERENCES Product (ProductId),
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

-- Chỉ được xóa sản phẩm khi không có bất kỳ Purchase nào và xóa Inventory trước 
IF OBJECT_ID('DeleteProductWithCheck', 'P') IS NOT NULL
    DROP PROCEDURE DeleteProductWithCheck;
GO
CREATE PROCEDURE DeleteProductWithCheck
    @ProductID INT
AS
BEGIN
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
        -- Sản phẩm có giao dịch mua hàng liên quan, tạo lỗi hoặc thực hiện hành động phù hợp
        -- THROW 50000, 'Không thể xóa sản phẩm có giao dịch mua hàng liên quan.', 1;
        -- Hoặc có thể sử dụng PRINT hoặc SELECT để hiển thị thông điệp
        PRINT 'Không thể xóa sản phẩm có giao dịch mua hàng liên quan.';
        -- SELECT 'Không thể xóa sản phẩm có giao dịch mua hàng liên quan.' AS ThongBao;
    END
END;
GO



