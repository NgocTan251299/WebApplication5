using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.scripts;

namespace WebApplication5.Controllers
{
    public class DangKyController : Controller
    {
        // GET: DangKy
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {

            return View();

        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Index(string name,DateTime dob,string gender,string address,string username,string pass,string repass)
        {
            var db = new BookContext();
            if (db.KhachHang.Where(i => i.TaiKhoan == username).FirstOrDefault()!=null)
            {
                ViewBag.Err = "User Name is duplicate, please try another one";
                return View("Index");
            }
            else
            {
                if (pass != repass)
                {
                    ViewBag.Err = "Repeat password is not correct ";
                    return View("Index");
                }
                else
                {
                    KhachHang kh = new KhachHang();
                    kh.flag = false;
                    kh.GioiTinh = gender;
                    kh.LoaiKH = "Khách Hàng";
                    kh.HoTen = name;
                    kh.NgaySinh = dob;
                    kh.TaiKhoan = username;
                    kh.MatKhau = pass;
                    using (db)
                    {
                        Log log = new Log();
                        log.TacVu = "Admin";
                        log.ThoiGian = DateTime.Now;
                        log.TaiKhoan = kh.TaiKhoan;
                        log.HanhDong = kh.HoTen + " đã được đăng kí ";
                        db.Log.Add(log);
                        db.KhachHang.Add(kh);
                        db.SaveChanges();
                        return View("../DangNhap/Index");
                    }
                    
                }
            }
        }
        ////public ActionResult DangKy(KhachHang kh, string rematkhau)
        ////{
        ////    DateTime dfDoB = new DateTime(0001, 1, 1);

        ////    using (var db = new BookContext())
        ////    {
        ////        bool flag = true;
        ////        foreach (var item in db.KhachHang)
        ////        {
        ////            if (item.TaiKhoan == kh.TaiKhoan)
        ////            {
        ////                flag = false;
        ////                break;
        ////            }
        ////        }
        ////        if (kh.HoTen == null || kh.TaiKhoan == null || kh.NgaySinh == dfDoB || kh.MatKhau == null || rematkhau == null)
        ////        {
        ////            if (kh.HoTen == null)
        ////                ViewBag.TenKH = "Vui lòng nhập họ tên";
        ////            if (kh.TaiKhoan == null)
        ////                ViewBag.TaiKhoan = "Vui lòng nhập tên tài khoản";
        ////            if (kh.NgaySinh == dfDoB)
        ////                ViewBag.NgaySinh = "Vui lòng chọn ngày sinh";
        ////            if (kh.MatKhau == null)
        ////                ViewBag.MatKhau = "Vui lòng nhập mật khẩu";
        ////            if (rematkhau == null)
        ////                ViewBag.rematkhau = "Vui lòng nhập lại mật khẩu";
        ////            return View("Index");
        ////        }
        ////        else if (rematkhau != kh.MatKhau)
        ////        {
        ////            ViewBag.rematkhau = "Mật khẩu không trùng khớp";
        ////            return View("Index");
        ////        }
        ////        else if (flag == false)
        ////        {
        ////            ViewBag.Err = "Tên tài khoản đã tồn tại";
        ////            return View("Index");
        ////        }
        ////        else
        ////        {
        ////            KhachHang KH = new KhachHang();
        ////            KH.HoTen = kh.HoTen;
        ////            KH.GioiTinh = kh.GioiTinh;
        ////            KH.TaiKhoan = kh.TaiKhoan;
        ////            string tmpPass = StringExtension.MD5Hash(kh.MatKhau).Substring(0, 20);

        ////            KH.MatKhau = tmpPass;
        ////            kh.LoaiKH = "Khách Hàng";
        ////            KH.flag = false;
        ////            db.KhachHang.Add(kh);
        ////            db.SaveChanges();
        ////            var tmp = db.KhachHang.ToList();
        ////        }
        ////        return RedirectToAction("Index", "DangNhap");
        ////    }

    } }
