USE [EmployeeDB]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1, N'.Net')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (2, N'NodeJs')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (3, N'Flutter')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (4, N'React')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (5, N'WordPress')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (6, N'Prestashop')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (7, N'RH')
SET IDENTITY_INSERT [dbo].[Department] OFF

SET IDENTITY_INSERT [dbo].[Employee] ON 
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Department], [DateOfJoining], [PhotoFileName]) VALUES (1, N'Sofien', N'.Net', '20180101', N'anonymous.png')
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Department], [DateOfJoining], [PhotoFileName]) VALUES (2, N'Ahmed', N'NodeJs', '20180101', N'anonymous.png')
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Department], [DateOfJoining], [PhotoFileName]) VALUES (3, N'Khadija', N'WordPress', '20180101', N'anonymous.png')
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Department], [DateOfJoining], [PhotoFileName]) VALUES (4, N'Mahmoud', N'Prestashop', '20180101', N'anonymous.png')
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Department], [DateOfJoining], [PhotoFileName]) VALUES (5, N'Majdi', N'Flutter', '20180101', N'anonymous.png')
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Department], [DateOfJoining], [PhotoFileName]) VALUES (6, N'Haitham', N'Prestashop', '20180101', N'anonymous.png')
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Department], [DateOfJoining], [PhotoFileName]) VALUES (7, N'Maryam', N'RH', '20180101', N'anonymous.png')
SET IDENTITY_INSERT [dbo].[Employee] OFF
