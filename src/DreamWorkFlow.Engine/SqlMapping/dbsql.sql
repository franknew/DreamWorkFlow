﻿CREATE TABLE WorkflowDefinition(
ID VARCHAR(32) NOT NULL Primary Key,
Name NVARCHAR(50) NULL,
Enabled BIT NULL,
Remark VARCHAR(500) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);

CREATE TABLE ActivityDefinition(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
WorkflowDefinitionID VARCHAR(32) NULL,
Page VARCHAR(200) NULL,
Name NVARCHAR(50) NULL,
Enabled BIT NULL,
Type INT NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Remark NVARCHAR(500) NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL,
Title NVARCHAR(200) NULL
);

CREATE TABLE LinkDefinition(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
FromActivityDefinitionID VARCHAR(32) NULL,
ToActivityDefinitionID VARCHAR(32) NULL,
Name NVARCHAR(50) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Remark NVARCHAR(500) NULL,
WorkflowDefinitionID VARCHAR(32),
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);

CREATE TABLE Workflow(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
WorkflowDefinitionID VARCHAR(32) NULL,
Name NVARCHAR(50) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Status INT NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);

CREATE TABLE Activity(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
ActivityDefinitionID VARCHAR(32) NULL,
Name NVARCHAR(50) NULL,
Page VARCHAR(200) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Type INT NULL,
WorkflowID VARCHAR(32) NULL,
Status INT NULL,
ReadTime DATETIME NULL,
ProcessTime DATETIME NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL,
Title NVARCHAR(200) NULL
);

CREATE TABLE Link(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
LinkDefinitionID VARCHAR(32) NULL,
FromActivityID VARCHAR(32) NULL,
ToAcivityID VARCHAR(32) NULL,
Name NVARCHAR(50) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Passed BIT NULL,
PassedTime DATETIME NULL,
WorkflowID VARCHAR(32) NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);

CREATE TABLE Approval(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
ActivityID VARCHAR(32) NULL,
Status INT NULL,
Remark NVARCHAR(500) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
WorkflowID VARCHAR(32) NULL,
Type INT NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);

CREATE TABLE Context(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
WorkflowID VARCHAR(32) NULL
);

CREATE TABLE Parameter(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
ContextID VARCHAR(32) NULL,
`Key` VARCHAR(50) NULL,
Value NVARCHAR(4000) NULL
);

CREATE TABLE Task(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
AcitivityID VARCHAR(32) NULL,
Name NVARCHAR(50) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Remark NVARCHAR(500) NULL,
`Status` INT NULL,
ReadTime DATETIME NULL,
ProcessTime DATETIME NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL,
UserID VARCHAR(32) NULL,
Title NVARCHAR(200) NULL
);

CREATE TABLE ActivityAuthDefinition(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
Name NVARCHAR(50) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
`Type` VARCHAR(10) NULL,
Value VARCHAR(50) NULL,
ActivityDefinitionID VARCHAR(32) NULL,
WorkflowDefinitionID VARCHAR(32) NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);

CREATE TABLE ActivityAuth(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
Name NVARCHAR(50) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
ActivityAuthDefinitionID VARCHAR(32) NULL,
Type VARCHAR(10) NULL,
Value VARCHAR(50) NULL,
ActivityID VARCHAR(32) NULL,
WorkflowID VARCHAR(32) NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);

CREATE TABLE DataDictionaryGroup
(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
Name VARCHAR(20) NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Remark NVARCHAR(500) NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL
);


CREATE TABLE DataDictionary
(
ID VARCHAR(32) NOT NULL PRIMARY KEY,
Name VARCHAR(20) NULL,
`Value` INT NULL,
Creator VARCHAR(32) NULL,
CreateTime DATETIME NULL,
Remark NVARCHAR(500) NULL,
LastUpdateTime DATETIME NULL,
LastUpdator VARCHAR(32) NULL,
DataDictionaryGroupID VARCHAR(32) NULL
);