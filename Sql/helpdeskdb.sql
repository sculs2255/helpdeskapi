-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 22, 2021 at 12:02 PM
-- Server version: 10.4.17-MariaDB
-- PHP Version: 7.4.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `helpdeskdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `bdsystem`
--

CREATE TABLE `bdsystem` (
  `BDSystemID` int(11) NOT NULL,
  `BDID` int(11) NOT NULL,
  `SystemID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `branch`
--

CREATE TABLE `branch` (
  `BranchID` int(11) NOT NULL,
  `CountryID` int(11) NOT NULL,
  `BranchName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `branchdepartment`
--

CREATE TABLE `branchdepartment` (
  `BDID` int(11) NOT NULL,
  `BranchID` int(11) NOT NULL,
  `DepartmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `cancel`
--

CREATE TABLE `cancel` (
  `CancelID` int(11) NOT NULL,
  `CaseID` int(11) NOT NULL,
  `Reason` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `case`
--

CREATE TABLE `case` (
  `CaseID` int(11) NOT NULL,
  `CaseTypeID` int(11) NOT NULL,
  `Date` date NOT NULL,
  `PriorityID` int(11) NOT NULL,
  `StatusID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `casetype`
--

CREATE TABLE `casetype` (
  `CaseTypeID` int(11) NOT NULL,
  `CaseTypeName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `comment`
--

CREATE TABLE `comment` (
  `CommentID` int(11) NOT NULL,
  `CaseID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `Title` varchar(45) NOT NULL,
  `Detail` varchar(45) NOT NULL,
  `File` varchar(45) DEFAULT NULL,
  `Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `country`
--

CREATE TABLE `country` (
  `CountryID` int(11) NOT NULL,
  `CountryName` varchar(45) CHARACTER SET ascii NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department` (
  `DepartmentID` int(11) NOT NULL,
  `DepartmentName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `incidentcase`
--

CREATE TABLE `incidentcase` (
  `ICID` int(11) NOT NULL,
  `CaseID` int(11) NOT NULL,
  `SystemID` int(11) NOT NULL,
  `ModuleID` int(11) NOT NULL,
  `ProgramID` varchar(45) NOT NULL,
  `Topic` varchar(45) NOT NULL,
  `Description` varchar(45) NOT NULL,
  `File` varchar(45) DEFAULT NULL,
  `Note` varchar(45) DEFAULT NULL,
  `CCMail` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `informer`
--

CREATE TABLE `informer` (
  `InformerID` int(11) NOT NULL,
  `CaseID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `informerworkplace`
--

CREATE TABLE `informerworkplace` (
  `IWID` int(11) NOT NULL,
  `InformerID` int(11) NOT NULL,
  `CountryID` int(11) NOT NULL,
  `BranchID` int(11) NOT NULL,
  `DepartmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `module`
--

CREATE TABLE `module` (
  `ModuleID` int(11) NOT NULL,
  `ModuleCode` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `priority`
--

CREATE TABLE `priority` (
  `PriorityID` int(11) NOT NULL,
  `PriorityName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `receiver`
--

CREATE TABLE `receiver` (
  `ReceiverID` int(11) NOT NULL,
  `CaseID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `requestcase`
--

CREATE TABLE `requestcase` (
  `RCID` int(11) NOT NULL,
  `CaseID` int(11) NOT NULL,
  `SystemID` int(11) NOT NULL,
  `TopicID` int(11) NOT NULL,
  `Description` varchar(45) NOT NULL,
  `File` varchar(45) DEFAULT NULL,
  `Note` varchar(45) DEFAULT NULL,
  `CCMail` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `require`
--

CREATE TABLE `require` (
  `RequiredID` int(11) NOT NULL,
  `CommentID` int(11) NOT NULL,
  `MoreInfo` varchar(45) DEFAULT NULL,
  `CaseFile` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `status`
--

CREATE TABLE `status` (
  `StatusID` int(11) NOT NULL,
  `StatusName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `system`
--

CREATE TABLE `system` (
  `SystemID` int(11) NOT NULL,
  `SystemName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `systemmodule`
--

CREATE TABLE `systemmodule` (
  `SMID` int(11) NOT NULL,
  `SystemID` int(11) NOT NULL,
  `Module` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `topic`
--

CREATE TABLE `topic` (
  `TopicID` int(11) NOT NULL,
  `TopicName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `UserID` int(11) NOT NULL,
  `Username` varchar(25) NOT NULL,
  `Password` varchar(25) NOT NULL,
  `UserTypeID` int(11) NOT NULL,
  `Active` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `usercontact`
--

CREATE TABLE `usercontact` (
  `UserContactID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Phone` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `userinfo`
--

CREATE TABLE `userinfo` (
  `UserInfoID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `Firstname` varchar(45) NOT NULL,
  `Lastname` varchar(45) NOT NULL,
  `Gender` varchar(10) NOT NULL,
  `UserPicture` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `usertype`
--

CREATE TABLE `usertype` (
  `UserTypeID` int(11) NOT NULL,
  `UserTypeName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `userworkplace`
--

CREATE TABLE `userworkplace` (
  `UserWorkplaceID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `CountryID` int(11) NOT NULL,
  `BranchID` int(11) NOT NULL,
  `DepartmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `workplace`
--

CREATE TABLE `workplace` (
  `WorkplaceID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `WorkplaceName` varchar(45) NOT NULL,
  `CountryID` int(11) NOT NULL,
  `BranchID` int(11) NOT NULL,
  `DepartmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bdsystem`
--
ALTER TABLE `bdsystem`
  ADD PRIMARY KEY (`BDSystemID`),
  ADD KEY `BDID_idx` (`BDID`),
  ADD KEY `SystemID_idx` (`SystemID`);

--
-- Indexes for table `branch`
--
ALTER TABLE `branch`
  ADD PRIMARY KEY (`BranchID`),
  ADD KEY `CountryID_idx` (`CountryID`);

--
-- Indexes for table `branchdepartment`
--
ALTER TABLE `branchdepartment`
  ADD PRIMARY KEY (`BDID`),
  ADD KEY `BranchID_idx` (`BranchID`),
  ADD KEY `DepartmentID_idx` (`DepartmentID`);

--
-- Indexes for table `cancel`
--
ALTER TABLE `cancel`
  ADD PRIMARY KEY (`CancelID`),
  ADD KEY `CaseID_idx` (`CaseID`);

--
-- Indexes for table `case`
--
ALTER TABLE `case`
  ADD PRIMARY KEY (`CaseID`),
  ADD KEY `CaseTypeID_idx` (`CaseTypeID`),
  ADD KEY `PriorityID_idx` (`PriorityID`),
  ADD KEY `StatusID_idx` (`StatusID`);

--
-- Indexes for table `casetype`
--
ALTER TABLE `casetype`
  ADD PRIMARY KEY (`CaseTypeID`);

--
-- Indexes for table `comment`
--
ALTER TABLE `comment`
  ADD PRIMARY KEY (`CommentID`),
  ADD KEY `CaseID_idx` (`CaseID`),
  ADD KEY `UserID_idx` (`UserID`);

--
-- Indexes for table `country`
--
ALTER TABLE `country`
  ADD PRIMARY KEY (`CountryID`);

--
-- Indexes for table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`DepartmentID`);

--
-- Indexes for table `incidentcase`
--
ALTER TABLE `incidentcase`
  ADD PRIMARY KEY (`ICID`),
  ADD KEY `CaseID_idx` (`CaseID`),
  ADD KEY `SystemID_idx` (`SystemID`),
  ADD KEY `ModuleID_idx` (`ModuleID`);

--
-- Indexes for table `informer`
--
ALTER TABLE `informer`
  ADD PRIMARY KEY (`InformerID`),
  ADD KEY `CaseID_idx` (`CaseID`),
  ADD KEY `UserID_idx` (`UserID`);

--
-- Indexes for table `informerworkplace`
--
ALTER TABLE `informerworkplace`
  ADD PRIMARY KEY (`IWID`),
  ADD KEY `InformerID_idx` (`InformerID`),
  ADD KEY `CountryID_idx` (`CountryID`),
  ADD KEY `BranchID_idx` (`BranchID`),
  ADD KEY `DepartmentID_idx` (`DepartmentID`);

--
-- Indexes for table `module`
--
ALTER TABLE `module`
  ADD PRIMARY KEY (`ModuleID`);

--
-- Indexes for table `priority`
--
ALTER TABLE `priority`
  ADD PRIMARY KEY (`PriorityID`);

--
-- Indexes for table `receiver`
--
ALTER TABLE `receiver`
  ADD PRIMARY KEY (`ReceiverID`),
  ADD KEY `CaseID_idx` (`CaseID`),
  ADD KEY `UserID_idx` (`UserID`);

--
-- Indexes for table `requestcase`
--
ALTER TABLE `requestcase`
  ADD PRIMARY KEY (`RCID`),
  ADD KEY `CaseID_idx` (`CaseID`),
  ADD KEY `SystemID_idx` (`SystemID`),
  ADD KEY `TopicID_idx` (`TopicID`);

--
-- Indexes for table `require`
--
ALTER TABLE `require`
  ADD PRIMARY KEY (`RequiredID`),
  ADD KEY `CommentID_idx` (`CommentID`);

--
-- Indexes for table `status`
--
ALTER TABLE `status`
  ADD PRIMARY KEY (`StatusID`);

--
-- Indexes for table `system`
--
ALTER TABLE `system`
  ADD PRIMARY KEY (`SystemID`);

--
-- Indexes for table `systemmodule`
--
ALTER TABLE `systemmodule`
  ADD PRIMARY KEY (`SMID`),
  ADD KEY `SystemID_idx` (`SystemID`),
  ADD KEY `Module_idx` (`Module`);

--
-- Indexes for table `topic`
--
ALTER TABLE `topic`
  ADD PRIMARY KEY (`TopicID`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UserID`),
  ADD KEY `UserTypeID_idx` (`UserTypeID`);

--
-- Indexes for table `usercontact`
--
ALTER TABLE `usercontact`
  ADD PRIMARY KEY (`UserContactID`),
  ADD KEY `UserID_idx` (`UserID`);

--
-- Indexes for table `userinfo`
--
ALTER TABLE `userinfo`
  ADD PRIMARY KEY (`UserInfoID`),
  ADD KEY `UserID_idx` (`UserID`);

--
-- Indexes for table `usertype`
--
ALTER TABLE `usertype`
  ADD PRIMARY KEY (`UserTypeID`);

--
-- Indexes for table `userworkplace`
--
ALTER TABLE `userworkplace`
  ADD PRIMARY KEY (`UserWorkplaceID`),
  ADD KEY `UserID_idx` (`UserID`),
  ADD KEY `CountryID_idx` (`CountryID`),
  ADD KEY `BranchID_idx` (`BranchID`),
  ADD KEY `DepartmentID_idx` (`DepartmentID`);

--
-- Indexes for table `workplace`
--
ALTER TABLE `workplace`
  ADD PRIMARY KEY (`WorkplaceID`),
  ADD KEY `UserID_idx` (`UserID`),
  ADD KEY `CountryID_idx` (`CountryID`),
  ADD KEY `BranchID_idx` (`BranchID`),
  ADD KEY `DepartmentID_idx` (`DepartmentID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `branch`
--
ALTER TABLE `branch`
  MODIFY `BranchID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `branchdepartment`
--
ALTER TABLE `branchdepartment`
  MODIFY `BDID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cancel`
--
ALTER TABLE `cancel`
  MODIFY `CancelID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `case`
--
ALTER TABLE `case`
  MODIFY `CaseID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `casetype`
--
ALTER TABLE `casetype`
  MODIFY `CaseTypeID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `comment`
--
ALTER TABLE `comment`
  MODIFY `CommentID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `country`
--
ALTER TABLE `country`
  MODIFY `CountryID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `department`
--
ALTER TABLE `department`
  MODIFY `DepartmentID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `incidentcase`
--
ALTER TABLE `incidentcase`
  MODIFY `ICID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `informer`
--
ALTER TABLE `informer`
  MODIFY `InformerID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `module`
--
ALTER TABLE `module`
  MODIFY `ModuleID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `priority`
--
ALTER TABLE `priority`
  MODIFY `PriorityID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `receiver`
--
ALTER TABLE `receiver`
  MODIFY `ReceiverID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `requestcase`
--
ALTER TABLE `requestcase`
  MODIFY `RCID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `require`
--
ALTER TABLE `require`
  MODIFY `RequiredID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `status`
--
ALTER TABLE `status`
  MODIFY `StatusID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `system`
--
ALTER TABLE `system`
  MODIFY `SystemID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `systemmodule`
--
ALTER TABLE `systemmodule`
  MODIFY `SMID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `topic`
--
ALTER TABLE `topic`
  MODIFY `TopicID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `usercontact`
--
ALTER TABLE `usercontact`
  MODIFY `UserContactID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `userinfo`
--
ALTER TABLE `userinfo`
  MODIFY `UserInfoID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `usertype`
--
ALTER TABLE `usertype`
  MODIFY `UserTypeID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `userworkplace`
--
ALTER TABLE `userworkplace`
  MODIFY `UserWorkplaceID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `workplace`
--
ALTER TABLE `workplace`
  MODIFY `WorkplaceID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserTypeID`) REFERENCES `usertype` (`UserTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `userinfo`
--
ALTER TABLE `userinfo`
  ADD CONSTRAINT `userinfo_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
