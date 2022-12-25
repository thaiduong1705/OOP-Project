USE [JOB_RECOMMENDATION]
SET DATEFORMAT dmy; 
GO

---CHUC VU---
INSERT INTO [dbo].[CHUCVU] ([TenChucVu]) VALUES ('Admin')
INSERT INTO [dbo].[CHUCVU] ([TenChucVu]) VALUES ('Staff')
GO

---NHAN VIEN---
INSERT INTO [dbo].[NHANVIEN]
           ([TenNhanVien]
           ,[Tuoi]
           ,[SDT]
           ,[Email])
     VALUES
           ('Duong Ming Tai'
           ,17
           ,'0123584865'
           ,'thaigae@gmail.com')

INSERT INTO [dbo].[NHANVIEN]
           ([TenNhanVien]
           ,[Tuoi]
           ,[SDT]
           ,[Email])
     VALUES
           ('Nguyen Duy Minh Tan'
           ,22
           ,'0986516516'
           ,'badboy2k@gmail.com')

INSERT INTO [dbo].[NHANVIEN]
           ([TenNhanVien]
           ,[Tuoi]
           ,[SDT]
           ,[Email])
     VALUES
           ('Nguyen Le Khoa'
           ,20
           ,'0235156843'
           ,'khoauithehe@gmail.com')

INSERT INTO [dbo].[NHANVIEN]
           ([TenNhanVien]
           ,[Tuoi]
           ,[SDT]
           ,[Email])
     VALUES
           ('Bill Gates'
           ,43
           ,'095231568'
           ,'windowsbetterthanmac@outlook.com')
GO

---TAI KHOAN---
INSERT INTO [dbo].[TAIKHOAN]
           ([TenDangNhap]
           ,[MatKhau]
           ,[ChucVu]
           ,[MaNhanVien])
     VALUES
           ('mingtai'
           ,'mingtai@'
           ,1
           ,1)

INSERT INTO [dbo].[TAIKHOAN]
           ([TenDangNhap]
           ,[MatKhau]
           ,[ChucVu])
     VALUES
           ('staff'
           ,'staff'
           ,2)

INSERT INTO [dbo].[TAIKHOAN]
           ([TenDangNhap]
           ,[MatKhau]
           ,[ChucVu])
     VALUES
           ('admin'
           ,'admin'
           ,1)
GO

---CONG TY---
INSERT INTO [dbo].[HOSOCONGTY]
           ([TenCongTy]
           ,[Website]
           ,[DiaChi]
           ,[QuocTich]
           ,[CheDoDaiNgo]
           ,[MoTaThem])
     VALUES
           ('Microsoft'
           ,'https://www.microsoft.com/vi-vn'
           ,'Redmond, Washington, Mỹ'
           ,'Mỹ'
           ,N'Thuong luong thang 13'
           ,N'Công ty được sáng lập bởi Bill Gates và Paul Allen vào ngày 4 tháng 4 năm 1975. Nếu tính theo doanh thu thì Microsoft là hãng sản xuất phần mềm lớn nhất thế giới.')

INSERT INTO [dbo].[HOSOCONGTY]
           ([TenCongTy]
           ,[Website]
           ,[DiaChi]
           ,[QuocTich]
           ,[CheDoDaiNgo]
           ,[MoTaThem])
     VALUES
           ('Google'
           ,'https://www.google.com'
           ,'Mountain View, California, Mỹ'
           ,'Mỹ'
           ,''
           ,'Google LLC là một công ty công nghệ đa quốc gia của Mỹ, chuyên về các dịch vụ và sản phẩm liên quan đến Internet, bao gồm các công nghệ quảng cáo trực tuyến, công cụ tìm kiếm, điện toán đám mây, phần mềm và phần cứng.')

INSERT INTO [dbo].[HOSOCONGTY]
           ([TenCongTy]
           ,[Website]
           ,[DiaChi]
           ,[QuocTich]
           ,[CheDoDaiNgo]
           ,[MoTaThem])
     VALUES
           ('Twitter Inc.'
           ,'https://twitter.com/home?lang=vi'
           ,'San Francisco, California, Mỹ'
           ,'Mỹ'
           ,''
           ,'Trụ sở của Twitter được đặt ở San Francisco và có hơn 35 văn phòng khắp thế giới.')

INSERT INTO [dbo].[HOSOCONGTY]
           ([TenCongTy]
           ,[Website]
           ,[DiaChi]
           ,[QuocTich]
           ,[CheDoDaiNgo]
           ,[MoTaThem])
     VALUES
           ('Meta Platforms'
           ,'https://twitter.com'
           ,'Menlo Park, California, Mỹ'
           ,'Mỹ'
           ,''
           ,'Đây được coi là một trong những công ty công nghệ Big Four cùng với Amazon, Apple và Google. Meta là một trong những công ty có giá trị nhất thế giới.')

INSERT INTO [dbo].[HOSOCONGTY]
           ([TenCongTy]
           ,[Website]
           ,[DiaChi]
           ,[QuocTich]
           ,[CheDoDaiNgo]
           ,[MoTaThem])
     VALUES
           ('Apple Inc'
           ,'https://www.apple.com'
           ,'Cupertino, California, Mỹ'
           ,'Mỹ'
           ,''
           ,'Chuyên thiết kế, phát triển và bán thiết bị điện tử tiêu dùng, phần mềm máy tính và các dịch vụ trực tuyến.')

GO

---BAI DANG---
INSERT INTO [dbo].[BAIDANG]
           ([MaCongTy]
           ,[TieuDe]
           ,[MoTa]
           ,[ViecLam]
           ,[LuongMin]
           ,[LuongMax]
           ,[ThamNien]
           ,[WebsiteBaiGoc]
           ,[NgayDangBai]
           ,[GhiChu]
           ,[MaTaiKhoan])
     VALUES
           (1
           ,'PD Incentive Planning & Management Manager'
           ,'Incentive Planning & Management:
- Work with Partners, Sales team and Management team to design and deploy incentive & recognition quarterly/annually plan to follow the Company’s Target in order to encourage internal salesforce and partners including sales contest, sales and promotion campaigns

- Manage and coordinate with other stakeholders to timely provide communication content/updated results in relation to incentive scheme to maximize the impact of sales campaigns/activities.

- Work with the Channel Market & Communication, Management System Information (MIS), Finance to ensure the timely delivery of contest memorandum/contest results, contest updates payment, promotion and trip execution.

- Regularly analyze the effectiveness of contests proven by data, business insightand propose solution for improvements in Working committees/Steering committees with Bank Partners.'
           ,'Incentive Planning & Management'
           ,10000
           ,30000
           ,4
           ,'https://www.vietnamworks.com/pd-incentive-planning-management-manager-1-1592879-jv/?source=searchResults&searchType=2&placement=1592880&sortBy=date'
           ,'19/12/2022'
           ,'Qualifications: Bachelor’s Degree in economics, Business Administration, Banking & Finance'
           ,1)

INSERT INTO [dbo].[BAIDANG]
           ([MaCongTy]
           ,[TieuDe]
           ,[MoTa]
           ,[ViecLam]
           ,[LuongMin]
           ,[LuongMax]
           ,[ThamNien]
           ,[WebsiteBaiGoc]
           ,[NgayDangBai]
           ,[GhiChu]
           ,[MaTaiKhoan])
     VALUES
           (2
           ,'SEO Specialist'
           ,'Responsibilities:
● Responsible for the search engine optimization of the company''s and client''s website.
● Maintain company''s and client''s website position as high as possible in competitive keywords.'
		   ,'SEO Specialist'
           ,15000
           ,40000
           ,2
           ,'https://www.vietnamworks.com/seo-specialist-81-1597982-jv/?source=searchResults&searchType=2&placement=1597983&sortBy=date'
           ,'30/11/2022'
           ,'Requirements: Required language(s): English & Vietnamese'
           ,1)

INSERT INTO [dbo].[BAIDANG]
           ([MaCongTy]
           ,[TieuDe]
           ,[MoTa]
           ,[ViecLam]
           ,[LuongMin]
           ,[LuongMax]
           ,[ThamNien]
           ,[WebsiteBaiGoc]
           ,[NgayDangBai]
           ,[GhiChu]
           ,[MaTaiKhoan])
     VALUES
           (2
           ,'.NET Software Engineer'
           ,'Design, implement, and maintain software components in complex, distributed systems. Work closely with other groups (QA, Tech Support) to effectively diagnose and resolve software defects.'
		   ,'SEO Specialist'
           ,12000
           ,50000
           ,3
           ,'https://topdev.vn/viec-lam/net-software-engineer-kofax-2028869'
           ,'23/12/2022'
           ,'Requirements:At least 3 years of working experience as Software Engineer. Proficiency in C#/.NET programming with Visual Studio 2015 or above in Windows environment.'
           ,1)
GO

---UNG VIEN---
INSERT INTO [dbo].[UNGVIEN]
           ([Ten]
           ,[Tuoi]
           ,[DiaChi]
           ,[Email]
           ,[SDT]
           ,[ThamNien])
     VALUES
           ('Can Duc Quang'
           ,20
           ,'324 Hòa Hưng, Phường 13, Quận 10, Thành phố Hồ Chí Minh.'
           ,'ducquangsama@gmail.com'
           ,'0915623051'
           ,4)

INSERT INTO [dbo].[UNGVIEN]
           ([Ten]
           ,[Tuoi]
           ,[DiaChi]
           ,[Email]
           ,[SDT]
           ,[ThamNien])
     VALUES
           ('Nguyen Minh Duy'
           ,22
           ,'237 Nguyễn Văn Cừ, Phường Nguyễn Cư Trinh, Quận 1, Thành phố Hồ Chí Minh'
           ,'duy6820@gmail.com'
           ,'0916584321'
           ,2)

INSERT INTO [dbo].[UNGVIEN]
           ([Ten]
           ,[Tuoi]
           ,[DiaChi]
           ,[Email]
           ,[SDT]
           ,[ThamNien])
     VALUES
           ('Truong Duc Thien'
           ,19
           ,'741, Phước Hoà, huyện Phú Giáo, tỉnh Bình Dương.'
           ,'thienboydz2k3@gmail.com'
           ,'0912648321'
           ,0)

INSERT INTO [dbo].[UNGVIEN]
           ([Ten]
           ,[Tuoi]
           ,[DiaChi]
           ,[Email]
           ,[SDT]
           ,[ThamNien])
     VALUES
           ('Nguyen Huu Minh Sang'
           ,20
           ,'258 đường Nguyễn Trãi, quận 1, TP Hồ Chí Minh.'
           ,'sangbatcandoi@gmail.com'
           ,'0913548463'
           ,2)
GO

---UNG TUYEN---
INSERT INTO [dbo].[UNGTUYEN]
           ([MaUngVien]
           ,[MaBaiDang]
           ,[NgayUngTuyen]
           ,[ChapThuan])
     VALUES
           (1
           ,2
           ,'24/12/2022'
           ,1)

INSERT INTO [dbo].[UNGTUYEN]
           ([MaUngVien]
           ,[MaBaiDang]
           ,[NgayUngTuyen]
           ,[ChapThuan])
     VALUES
           (2
           ,2
           ,'24/12/2022'
           ,0)

INSERT INTO [dbo].[UNGTUYEN]
           ([MaUngVien]
           ,[MaBaiDang]
           ,[NgayUngTuyen])
     VALUES
           (3
           ,3
           ,'24/12/2022')
GO

---KY NANG---
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Data Analyst')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Java')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Tester')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('PHP')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Front-End')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Flutter')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Python')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('JavaScript')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('ReactJS')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('.NET')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Back-End')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('C#')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('NodeJS')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('React Native')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Product Manager')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Business Analyst')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('System Admin')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('DevOps')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('System Engineer')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Data Analyst')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Golang')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('AWS')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Azure')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('SEO Website')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('SEO')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Google Analytics')
INSERT INTO [dbo].[KINANG] ([TenKiNang]) VALUES ('Tester')
GO

---KI NANG YEU CAU CUA BAI DANG---
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (1, 1)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (15, 1)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (17, 1)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (18, 1)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (1, 2)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (24, 2)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (25, 2)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (26, 2)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (1, 3)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (10, 3)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (12, 3)
INSERT INTO [dbo].[KINANG_BAIDANG] ([MaKiNang], [MaBaiDang]) VALUES (18, 3)
GO

---KY NANG CUA UNG VIEN---
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (3, 1)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (1, 1)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (16, 1)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (3, 2)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (14, 2)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (16, 3)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (14, 3)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (6, 3)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (5, 4)
INSERT INTO [dbo].[KINANG_UNGVIEN]([MaKiNang], [MaUngVien]) VALUES (8, 4)
GO






