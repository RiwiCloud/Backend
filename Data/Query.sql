
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
    FOREIGN KEY (User_Id) REFERENCES Users(Id),
    CONSTRAINT FK_Folders_ParentFolder FOREIGN KEY (ParentFolder_Id) REFERENCES Folders(Id) ON DELETE CASCADE
);

-- Creación de la tabla Files
CREATE TABLE Files (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50),
    File_Id INT,
    Folder_Id INT,
    Status ENUM('Created','Delete'),
    DateCreated DATE,
    FOREIGN KEY (Folder_Id) REFERENCES Folders(Id) ON DELETE CASCADE
);


-- Datos de ejemplo para la tabla Users
INSERT INTO Users (Name, Email, Password, Status)
VALUES ('User1', 'user1@example.com', 'password1', 'active'),
       ('User2', 'user2@example.com', 'password2', 'inactive');

-- Datos de ejemplo para la tabla Users
INSERT INTO Users (Name, Email, Password, Status, DateCreated) VALUES 
('Alice Johnson', 'alice.johnson@example.com', 'password1', 'Active', '2023-01-01'),
('Bob Smith', 'bob.smith@example.com', 'password2', 'Active', '2023-01-02'),
('Charlie Brown', 'charlie.brown@example.com', 'password3', 'Inactive', '2023-01-03');

-- Datos de ejemplo para la tabla Folders
INSERT INTO Folders (Name, User_Id, ParentFolder_Id, Status, DateCreated) VALUES 
('Documents', 1, NULL, 'Active', '2023-01-01'),
('Projects', 1, 1, 'Active', '2023-01-02'),
('Music', 2, NULL, 'Active', '2023-01-03');

-- Datos de ejemplo para la tabla Files
INSERT INTO Files (Name, Folder_Id, Status, DateCreated) VALUES 
('file1.txt', 1, 'Created', '2023-01-01'),
('file2.txt', 2, 'Created', '2023-01-02'),
('file3.mp3', 3, 'Created', '2023-01-03');

