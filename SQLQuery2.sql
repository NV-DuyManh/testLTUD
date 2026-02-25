-- 1. Xóa các bảng cũ nếu tồn tại (theo thứ tự ngược để tránh lỗi khóa ngoại)
IF OBJECT_ID('lichchieu', 'U') IS NOT NULL DROP TABLE lichchieu;
IF OBJECT_ID('ghe', 'U') IS NOT NULL DROP TABLE ghe;
IF OBJECT_ID('phim', 'U') IS NOT NULL DROP TABLE phim;
IF OBJECT_ID('sanpham', 'U') IS NOT NULL DROP TABLE sanpham;
IF OBJECT_ID('phongchieu', 'U') IS NOT NULL DROP TABLE phongchieu;
IF OBJECT_ID('theloai', 'U') IS NOT NULL DROP TABLE theloai;
IF OBJECT_ID('nguoidung', 'U') IS NOT NULL DROP TABLE nguoidung;

-- 2. Tạo bảng nguoidung
CREATE TABLE nguoidung (
    ma_nguoi_dung INT IDENTITY(1,1) PRIMARY KEY,
    tai_khoan NVARCHAR(50) NOT NULL UNIQUE,
    mat_khau NVARCHAR(255) NOT NULL,
    ho_ten NVARCHAR(100) NULL,
    chuc_vu NVARCHAR(10) NOT NULL CHECK (chuc_vu IN ('Admin', 'Staff')) DEFAULT 'Staff'
);

-- 3. Tạo bảng theloai
CREATE TABLE theloai (
    ma_the_loai INT IDENTITY(1,1) PRIMARY KEY,
    ten_the_loai NVARCHAR(50) NOT NULL
);

-- 4. Tạo bảng phongchieu
CREATE TABLE phongchieu (
    ma_phong INT IDENTITY(1,1) PRIMARY KEY,
    ten_phong NVARCHAR(100) NOT NULL,
    tinh_trang NVARCHAR(20) CHECK (tinh_trang IN ('HoatDong', 'BaoTri')) DEFAULT 'HoatDong',
    nguoi_quan_ly INT NULL,
    CONSTRAINT FK_Phong_User FOREIGN KEY (nguoi_quan_ly) REFERENCES nguoidung (ma_nguoi_dung) ON DELETE SET NULL
);

-- 5. Tạo bảng phim
CREATE TABLE phim (
    ma_phim INT IDENTITY(1,1) PRIMARY KEY,
    ten_phim NVARCHAR(255) NOT NULL,
    ma_the_loai INT NOT NULL,
    thoi_luong INT NOT NULL,
    ngay_khoi_chieu DATE NOT NULL,
    ngay_ket_thuc DATE NULL,
    mo_ta NVARCHAR(MAX) NULL,
    trang_thai NVARCHAR(20) CHECK (trang_thai IN ('DangChieu', 'SapChieu', 'NgungChieu')) DEFAULT 'DangChieu',
    nguoi_nhap INT NULL,
    CONSTRAINT FK_Phim_TheLoai FOREIGN KEY (ma_the_loai) REFERENCES theloai (ma_the_loai),
    CONSTRAINT FK_Phim_User FOREIGN KEY (nguoi_nhap) REFERENCES nguoidung (ma_nguoi_dung) ON DELETE SET NULL
);

-- 6. Tạo bảng ghe
CREATE TABLE ghe (
    ma_ghe INT IDENTITY(1,1) PRIMARY KEY,
    ma_phong INT NOT NULL,
    ten_ghe NVARCHAR(10) NOT NULL,
    loai_ghe NVARCHAR(10) CHECK (loai_ghe IN ('Thuong', 'VIP', 'Doi')) DEFAULT 'Thuong',
    trang_thai NVARCHAR(10) CHECK (trang_thai IN ('KhaDung', 'Hong')) DEFAULT 'KhaDung',
    CONSTRAINT FK_Ghe_Phong FOREIGN KEY (ma_phong) REFERENCES phongchieu (ma_phong) ON DELETE CASCADE,
    CONSTRAINT UK_Ghe_Trong_Phong UNIQUE (ma_phong, ten_ghe)
);

-- 7. Tạo bảng lichchieu
CREATE TABLE lichchieu (
    ma_lich_chieu INT IDENTITY(1,1) PRIMARY KEY,
    ma_phim INT NOT NULL,
    ma_phong INT NOT NULL,
    nguoi_lap_lich INT NOT NULL,
    ngay_chieu DATE NOT NULL,
    gio_bat_dau TIME NOT NULL,
    gia_ve_co_ban DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_Lich_Phim FOREIGN KEY (ma_phim) REFERENCES phim (ma_phim) ON DELETE CASCADE,
    CONSTRAINT FK_Lich_Phong FOREIGN KEY (ma_phong) REFERENCES phongchieu (ma_phong) ON DELETE CASCADE,
    CONSTRAINT FK_Lich_User FOREIGN KEY (nguoi_lap_lich) REFERENCES nguoidung (ma_nguoi_dung),
    CONSTRAINT UK_Lich_Chieu_Unique UNIQUE (ma_phong, ngay_chieu, gio_bat_dau)
);

-- 8. Tạo bảng sanpham
CREATE TABLE sanpham (
    ma_san_pham INT IDENTITY(1,1) PRIMARY KEY,
    ten_san_pham NVARCHAR(100) NOT NULL,
    loai NVARCHAR(10) CHECK (loai IN ('DoAn', 'NuocUong', 'Combo')) NOT NULL,
    gia_ban DECIMAL(10, 2) NOT NULL,
    so_luong_ton INT NOT NULL DEFAULT 0,
    trang_thai NVARCHAR(20) CHECK (trang_thai IN ('DangBan', 'NgungBan')) DEFAULT 'DangBan',
    nguoi_cap_nhat INT NULL,
    CONSTRAINT FK_SanPham_User FOREIGN KEY (nguoi_cap_nhat) REFERENCES nguoidung (ma_nguoi_dung) ON DELETE SET NULL
);

-- 9. Chèn dữ liệu mẫu
INSERT INTO nguoidung (tai_khoan, mat_khau, chuc_vu) VALUES ('admin', '123', 'Admin'), ('staff', '123', 'Staff');
INSERT INTO theloai (ten_the_loai) VALUES (N'Hành Động'), (N'Tình Cảm'), (N'Hoạt Hình');
INSERT INTO phongchieu (ten_phong, tinh_trang, nguoi_quan_ly) VALUES (N'Rạp 1', 'HoatDong', 1);
INSERT INTO phim (ten_phim, ma_the_loai, thoi_luong, ngay_khoi_chieu, trang_thai, nguoi_nhap) VALUES (N'Mai', 2, 131, '2024-02-10', 'DangChieu', 1);
INSERT INTO ghe (ma_phong, ten_ghe, loai_ghe) VALUES (1, 'A1', 'Thuong'), (1, 'A2', 'Thuong'), (1, 'B1', 'VIP'), (1, 'B2', 'VIP');
INSERT INTO lichchieu (ma_phim, ma_phong, nguoi_lap_lich, ngay_chieu, gio_bat_dau, gia_ve_co_ban) VALUES (1, 1, 1, '2026-01-31', '19:00:00', 100000);
INSERT INTO sanpham (ten_san_pham, loai, gia_ban, so_luong_ton) VALUES (N'Bắp', 'DoAn', 45000, 100);