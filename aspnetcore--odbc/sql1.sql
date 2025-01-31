CREATE TABLE products (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    name NVARCHAR(100) NOT NULL,
    brand NVARCHAR(100) NOT NULL,
    category NVARCHAR(100) NOT NULL,
    price DECIMAL(16, 2) NOT NULL,
    description NVARCHAR(MAX) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
);