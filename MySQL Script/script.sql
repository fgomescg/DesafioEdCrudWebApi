-- MySQL Script generated by MySQL Workbench
-- Wed Sep 30 11:50:10 2020
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema DesafioEdCRUD
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema DesafioEdCRUD
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `DesafioEdCRUD` DEFAULT CHARACTER SET utf8 ;
USE `DesafioEdCRUD` ;

-- -----------------------------------------------------
-- Table `DesafioEdCRUD`.`Book`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `DesafioEdCRUD`.`Book` ;

CREATE TABLE IF NOT EXISTS `DesafioEdCRUD`.`Book` (
  `BookId` INT NOT NULL AUTO_INCREMENT,
  `Title` VARCHAR(40) NOT NULL,
  `Company` VARCHAR(40) NOT NULL,
  `Edition` INT NOT NULL,
  `PublishYear` VARCHAR(4) NOT NULL,
  `Value` DECIMAL(15,2) NOT NULL,
  PRIMARY KEY (`BookId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DesafioEdCRUD`.`Author`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `DesafioEdCRUD`.`Author` ;

CREATE TABLE IF NOT EXISTS `DesafioEdCRUD`.`Author` (
  `AuthorId` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(40) NOT NULL,
  PRIMARY KEY (`AuthorId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DesafioEdCRUD`.`Subject`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `DesafioEdCRUD`.`Subject` ;

CREATE TABLE IF NOT EXISTS `DesafioEdCRUD`.`Subject` (
  `SubjectId` INT NOT NULL AUTO_INCREMENT,
  `Description` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`SubjectId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DesafioEdCRUD`.`BookAuthor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `DesafioEdCRUD`.`BookAuthor` ;

CREATE TABLE IF NOT EXISTS `DesafioEdCRUD`.`BookAuthor` (
  `BookId` INT NOT NULL,
  `AuthorId` INT NOT NULL,
  PRIMARY KEY (`BookId`, `AuthorId`),
  INDEX `fk_Book_has_Author_Author1_idx` (`AuthorId` ASC) VISIBLE,
  INDEX `fk_Book_has_Author_Book1_idx` (`BookId` ASC) VISIBLE,
  CONSTRAINT `fk_Book_has_Author_Book1`
    FOREIGN KEY (`BookId`)
    REFERENCES `DesafioEdCRUD`.`Book` (`BookId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Book_has_Author_Author1`
    FOREIGN KEY (`AuthorId`)
    REFERENCES `DesafioEdCRUD`.`Author` (`AuthorId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DesafioEdCRUD`.`BookSubject`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `DesafioEdCRUD`.`BookSubject` ;

CREATE TABLE IF NOT EXISTS `DesafioEdCRUD`.`BookSubject` (
  `BookId` INT NOT NULL,
  `SubjectId` INT NOT NULL,
  PRIMARY KEY (`BookId`, `SubjectId`),
  INDEX `fk_Book_has_Subject_Subject1_idx` (`SubjectId` ASC) VISIBLE,
  INDEX `fk_Book_has_Subject_Book1_idx` (`BookId` ASC) VISIBLE,
  CONSTRAINT `fk_Book_has_Subject_Book1`
    FOREIGN KEY (`BookId`)
    REFERENCES `DesafioEdCRUD`.`Book` (`BookId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Book_has_Subject_Subject1`
    FOREIGN KEY (`SubjectId`)
    REFERENCES `DesafioEdCRUD`.`Subject` (`SubjectId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `DesafioEdCRUD` ;

-- -----------------------------------------------------
-- Placeholder table for view `DesafioEdCRUD`.`BookAuthorReport`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DesafioEdCRUD`.`BookAuthorReport` (`AuthorName` INT, `BookTitle` INT, `BookCompany` INT, `BookEdition` INT, `BookPublishYear` INT, `BookValue` INT, `BookSubject` INT);

-- -----------------------------------------------------
-- View `DesafioEdCRUD`.`BookAuthorReport`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `DesafioEdCRUD`.`BookAuthorReport`;
DROP VIEW IF EXISTS `DesafioEdCRUD`.`BookAuthorReport` ;
USE `DesafioEdCRUD`;
CREATE OR REPLACE VIEW BookAuthorReport AS
    SELECT 
        author.Name AS AuthorName,
        BOOK.Title AS BookTitle,
        BOOK.Company AS BookCompany,
        BOOK.Edition AS BookEdition,
        BOOK.PublishYear AS BookPublishYear,
        BOOK.Value AS BookValue,
        subject.Description AS BookSubject
    FROM
        book
            JOIN
        booksubject ON book.BookId = booksubject.BookId
            JOIN
        subject ON booksubject.SubjectId = subject.SubjectId
            JOIN
        bookauthor ON book.BookId = bookauthor.BookId
            JOIN
        author ON bookauthor.AuthorId = author.AuthorId
            AND author.AuthorId IS NOT NULL
    GROUP BY author.Name
    ORDER BY author.Name;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
