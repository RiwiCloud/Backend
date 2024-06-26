
-- Creación de la tabla Users
CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(145) ,
    Email VARCHAR(255)  UNIQUE,
    Password VARCHAR(16),
    Status ENUM('Active', 'Inactive'),
    DateCreated DATE
);

-- Creación de la tabla Folders
CREATE TABLE Folders (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50),
    User_Id INT,
    ParentFolder_Id INT NULL,
    Status ENUM('Active', 'Inactive'),
    DateCreated DATE,
    FOREIGN KEY (User_Id) REFERENCES Users(Id)
    CONSTRAINT FK_Folders_ParentFolder FOREIGN KEY (ParentFolderId) REFERENCES Folders(Id) ON DELETE CASCADE
);

-- Creación de la tabla Files
CREATE TABLE Files (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50),
    File_Id INT,
    Status ENUM("Created","Delete"),
    DateCreated DATE,
    FOREIGN KEY (Folder_Id) REFERENCES Folders(Id) ON DELETE CASCADE
);


-- Datos de ejemplo para la tabla Users
INSERT INTO Users (Name, Email, Password, Status)
VALUES ('User1', 'user1@example.com', 'password1', 'active'),
       ('User2', 'user2@example.com', 'password2', 'inactive');

-- Datos de ejemplo para la tabla Folders
INSERT INTO Folders (Name, User_Id, Status)
VALUES ('Folder1', 1, 'active'),
       ('Folder2', 2, 'inactive');

-- Datos de ejemplo para la tabla Files
INSERT INTO Files (Name, File_Id, Status)
VALUES ('File1', 1, 'active'),
       ('File2', 2, 'inactive');
