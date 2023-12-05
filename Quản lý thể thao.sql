CREATE TABLE Category (
    Category_id INTEGER PRIMARY KEY,
    Category_name VARCHAR UNIQUE NOT NULL
);

CREATE TABLE Product (
    Product_id INTEGER PRIMARY KEY,
    Product_name VARCHAR UNIQUE NOT NULL,
    Product_description TEXT,
    Product_price DECIMAL NOT NULL,
    Product_stock INTEGER NOT NULL,
    Category_id INTEGER,
    FOREIGN KEY (Category_id) REFERENCES Category (Category_id)
);

CREATE TABLE Customer (
    Customer_id INTEGER PRIMARY KEY,
    Cus_name VARCHAR UNIQUE NOT NULL,
    Email VARCHAR UNIQUE NOT NULL,
    Address TEXT NOT NULL,
    Phone VARCHAR NOT NULL,
    Role INTEGER NOT NULL,
    User_name VARCHAR UNIQUE NOT NULL,
    pass_word VARCHAR NOT NULL
);

CREATE TABLE Cart (
    Cart_id INTEGER PRIMARY KEY,
    Customer_id INTEGER,
    FOREIGN KEY (Customer_id) REFERENCES Customer (Customer_id)
);

CREATE TABLE Order_Item (
    Order_id INTEGER,
    Product_id INTEGER,
    Quantity DECIMAL NOT NULL,
    Quantity_price DECIMAL NOT NULL,
    PRIMARY KEY (Order_id, Product_id),
    FOREIGN KEY (Order_id) REFERENCES Order_ (Order_id),
    FOREIGN KEY (Product_id) REFERENCES Product (Product_id)
);

CREATE TABLE Order_ (
    Order_id INTEGER PRIMARY KEY,
    Customer_id INTEGER,
    Order_date DATE NOT NULL,
    Total_amount DECIMAL NOT NULL,
    Shipping_address TEXT NOT NULL,
    Payment_method VARCHAR NOT NULL,
    FOREIGN KEY (Customer_id) REFERENCES Customer (Customer_id)
);

CREATE TABLE Review (
    Review_id INTEGER PRIMARY KEY,
    Admin_id INTEGER,
    Product_id INTEGER,
    Customer_id INTEGER,
    Rating INTEGER NOT NULL,
    Comment TEXT,
    FOREIGN KEY (Admin_id) REFERENCES Admin (Admin_id),
    FOREIGN KEY (Product_id) REFERENCES Product (Product_id),
    FOREIGN KEY (Customer_id) REFERENCES Customer (Customer_id)
);

CREATE TABLE Admin (
    Admin_id INTEGER PRIMARY KEY,
    Name VARCHAR UNIQUE NOT NULL,
    Email VARCHAR UNIQUE NOT NULL,
    pass_word VARCHAR NOT NULL,
    Role INTEGER NOT NULL
);
ALTER TABLE Admin
ALTER COLUMN Name VARCHAR(255);

ALTER TABLE Admin
ALTER COLUMN Email VARCHAR(255);

ALTER TABLE Admin
ALTER COLUMN pass_word VARCHAR(255);

INSERT INTO Admin (Admin_id, Name, Email, pass_word, Role)
VALUES
    (1, 'Bùi Kiên', 'b@gmail.com', '123', 1),
    (2, 'Jane Smith', 'janesmith@example.com', 'password456', 2),
    (3, 'Mike Johnson', 'mikejohnson@example.com', 'password789', 1);

