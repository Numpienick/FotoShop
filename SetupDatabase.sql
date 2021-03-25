CREATE SCHEMA IF NOT EXISTS fotoshop;

DROP TABLE IF EXISTS Placed_order_photo;
DROP TABLE IF EXISTS placed_order;
DROP TABLE IF EXISTS account;
DROP TABLE IF EXISTS `order`;
DROP TABLE IF EXISTS `order_photo`;
DROP TABLE IF EXISTS `user`;
DROP TABLE IF EXISTS photo;
DROP TABLE IF EXISTS category;
DROP TABLE IF EXISTS contact;

CREATE TABLE account (
  Account_id int NOT NULL AUTO_INCREMENT,
  Email varchar(255) NOT NULL,
  Password varchar(45) NOT NULL,
  Account_type varchar(5) NOT NULL DEFAULT 'user',
  First_name varchar(20) NOT NULL,
  Last_name varchar(45) NOT NULL,
  PRIMARY KEY (Account_id),
  UNIQUE KEY Email_UNIQUE (Email),
  UNIQUE KEY Account_id_UNIQUE (Account_id)
);

CREATE TABLE category (
  Name varchar(20) NOT NULL,
  Description varchar(500) DEFAULT NULL,
  PRIMARY KEY (Name),
  UNIQUE KEY Name_UNIQUE (Name)
);

CREATE TABLE contact (
  Contact_id int NOT NULL AUTO_INCREMENT,
  Subject varchar(100) NOT NULL,
  Message varchar(999) NOT NULL,
  Name varchar(45) NOT NULL,
  Email varchar(255) NOT NULL,
  PRIMARY KEY (Contact_id),
  UNIQUE KEY Contact_id_UNIQUE (Contact_id)
);

CREATE TABLE placed_order (
  Placed_order_id int NOT NULL AUTO_INCREMENT,
  Account_id int NOT NULL,
  Date_placed_order_placed datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  Date_placed_order_paid datetime DEFAULT NULL,
  Download_link varchar(100) DEFAULT NULL,
  PRIMARY KEY (Placed_order_id),
  UNIQUE KEY Placed_order_id_UNIQUE (Placed_order_id),
  KEY Account_id_idx (Account_id),
  CONSTRAINT Account_id FOREIGN KEY (Account_id) REFERENCES account (Account_id)
);

CREATE TABLE photo (
  Photo_id int NOT NULL AUTO_INCREMENT,
  Photo_path varchar(100) NOT NULL,
  Price decimal(5,2) DEFAULT '0.00',
  Description varchar(500) DEFAULT NULL,
  Category_name varchar(20) NOT NULL,
  PRIMARY KEY (Photo_id),
  UNIQUE KEY Photo_id_UNIQUE (Photo_id),
  KEY Category_name_idx (Category_name),
  CONSTRAINT Category_name FOREIGN KEY (Category_name) REFERENCES category (Name)
);

CREATE TABLE placed_order_photo (
  Placed_order_id int NOT NULL,
  Photo_id int NOT NULL,
  PRIMARY KEY (Placed_order_id,Photo_id),
  KEY Photo_id_idx (Photo_id),
  CONSTRAINT Placed_order_id FOREIGN KEY (Placed_order_id) REFERENCES placed_order (Placed_order_id),
  CONSTRAINT Photo_id FOREIGN KEY (Photo_id) REFERENCES photo (Photo_id)
);

INSERT INTO account (Email, Password, Account_type, First_name, Last_name)
VALUES ("admin@admin.com", "123", "admin", "Fotograaf", "Persoon");

INSERT INTO category (Name, Description)
VALUES ("Urban", "Dit is een mooie Urban categorie");
INSERT INTO category (Name, Description)
VALUES ("Urbex", "Dit is een mooie Urbex categorie");
INSERT INTO category (Name, Description)
VALUES ("Pop", "Dit is een mooie Pop categorie");

INSERT INTO fotoshop.contact (Subject, Message, Name, Email)
VALUES ("Test", "Ik ben een test bericht", "Nick", "nick.numan@student.nhlstenden.com");

INSERT INTO placed_order (Account_id, Download_link)
VALUES ("1", "https://downloadhieruwfotos.nl");

INSERT INTO photo (Photo_path, Price, Description, Category_name)
VALUES ("~/Path/To/photo.jpg", "12.99", "Dit is een mooie foto", "Urban");

INSERT INTO placed_order_photo (Placed_order_id, Photo_id)
VALUES ("1", "1");