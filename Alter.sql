ALTER TABLE [User]  ADD [UnitID] int REFERENCES [Unit](UnitID)
ALTER TABLE [QuestionStepResult] ADD FOREIGN KEY (CreateUser) REFERENCES  [User](UserID);
ALTER TABLE [UserRole] ADD PRIMARY KEY (UserID,RoleID);
ALTER TABLE [ParentUnit] ADD PRIMARY KEY (ChildID);