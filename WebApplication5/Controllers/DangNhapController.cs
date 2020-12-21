using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class DangNhapController : Controller
    {
        BookContext db = new BookContext();
        // GET: DangNhap
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
           
                KhachHang khachHang = new KhachHang();
                khachHang.HoTen = "Admin";
                khachHang.NgaySinh = DateTime.Today;
                khachHang.GioiTinh = "Nam";
                khachHang.TaiKhoan = "admin";
                khachHang.MatKhau = "admin";
                khachHang.LoaiKH = "Admin";
                khachHang.flag = false;
            if (db.KhachHang.Where(i=>i.TaiKhoan==khachHang.TaiKhoan).FirstOrDefault() == null)
            {
                using (db)
                {
                    Log log = new Log();
                    log.TacVu = "Admin";
                    log.ThoiGian = DateTime.Now;
                    log.TaiKhoan = khachHang.TaiKhoan;
                    log.HanhDong = khachHang.HoTen + " đã được đăng kí ";
                    db.Log.Add(log);
                    db.KhachHang.Add(khachHang);
                    db.SaveChanges();
                }
            }
            if (Session["KhachHang"]!=null)
            {
                return RedirectToAction("Index", "Home");
            }
            else {  return View();}
           
            
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ActionResult Index(string username,string password)
        {
            if (Session["KhachHang"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var rs = db.KhachHang.Where(i => i.flag == false && i.TaiKhoan == username && i.MatKhau == password).FirstOrDefault();

                if (rs == null)
                {
                    ViewBag.Err = "Wrong User Name or Password";
                    return View("Index");
                }
                else
                {
                    Session["KhachHang"] = rs;
                    if (rs.LoaiKH == "Admin" || rs.LoaiKH == "Nhập liệu")
                    {
                        KhachHang kh1 = new KhachHang();
                        kh1 = Session["KhachHang"] as KhachHang;
                        using (var db = new BookContext())
                        {
                            Log log = new Log();
                            log.TacVu = "Admin";
                            log.ThoiGian = DateTime.Now;
                            log.TaiKhoan = kh1.TaiKhoan;
                            log.HanhDong = "Đã đăng nhập ";
                            db.Log.Add(log);
                            db.SaveChanges();
                            return View("../KhachHang/AdIndex");

                        }
                        
                    }
                    else
                    {
                        KhachHang kh1 = new KhachHang();
                        kh1 = Session["KhachHang"] as KhachHang;
                        using (var db = new BookContext())
                        {
                            Log log = new Log();
                            log.TacVu = "Admin";
                            log.ThoiGian = DateTime.Now;
                            log.TaiKhoan = kh1.TaiKhoan;
                            log.HanhDong = "Đã đăng nhập ";
                            db.Log.Add(log);
                            db.SaveChanges();
                            return View("../Home/Index");

                        }
                        
                    }
                }
            }
        }
        ////public ActionResult Authorise(string username, string password)
        ////{
        ////    if (username == string.Empty || password == string.Empty)
        ////    {
        ////        if (username == string.Empty)
        ////            ViewBag.TenKHError = "Vui lòng nhập tên tài khoản";
        ////        if (password == string.Empty)
        ////            ViewBag.MatKhauError = "Vui lòng nhập Mật khẩu";
        ////        return View("Index");
        ////    }
        ////    else if (username != string.Empty && password != string.Empty)
        ////    {
        ////        using (var db = new BookContext())
        ////        {
        ////            var temp = db.KhachHang.Where(i => i.TaiKhoan == username && i.MatKhau == password && i.flag==false).FirstOrDefault();
        ////            if (temp == null)
        ////            {
        ////                ViewBag.AccError = "Thông tin không đúng";
        ////                return View("Index");
        ////            }
        ////            else
        ////            {
        ////                KhachHang kh = new KhachHang();
        ////                kh = db.KhachHang.Find(temp.MaKH);
        ////                Session["KhachHang"] = kh;
        ////                Session["KhachHangId"] = kh.MaKH;
        ////                Session["TenKhachHang"] = kh.HoTen;
        ////                ViewBag.kh = kh.NgaySinh.Year.ToString() + "-" + kh.NgaySinh.Month.ToString() + "-" + kh.NgaySinh.Date.ToString();


        ////                HoaDon hd = db.HoaDon.Where(i => i.DaThanhToan == false && i.MaKH == kh.MaKH).FirstOrDefault();
        ////                if (hd == null)
        ////                {
        ////                    Session["SL"] = 0;
        ////                }
        ////                else
        ////                {
        ////                    int soluong = 0;
        ////                    foreach (var item in db.ChiTietHoaDon.Where(i=>i.flag==false && i.MaDonHang==hd.MaDonHang).ToList())
        ////                    {
        ////                        soluong = soluong + item.SoLuong;
        ////                    }
        ////                    Session["SL"] = soluong;
        ////                }


        ////                if (temp.LoaiKH == "Admin")
        ////                {
        ////                    Log log = new Log();
        ////                    log.TacVu = "Admin";
        ////                    log.ThoiGian = DateTime.Now;
        ////                    log.TaiKhoan = kh.TaiKhoan;
        ////                    log.HanhDong = "Đã đăng nhập vào hệ thống";
        ////                    db.Log.Add(log);
        ////                    db.SaveChanges();
        ////                    return RedirectToAction("AdIndex", "KhachHang");
        ////                }
        ////                else
        ////                {
        ////                    return RedirectToAction("Index", "Home");
        ////                }
        ////            }

        ////        }
        ////    }
        ////    return RedirectToAction("Index");
        ////}
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult DangXuat()
        {
            
            KhachHang kh1 = new KhachHang();
            kh1 = Session["KhachHang"] as KhachHang;
            using (var db = new BookContext())
            {
                Log log = new Log();
                log.TacVu = "Admin";
                log.ThoiGian = DateTime.Now;
                log.TaiKhoan = kh1.TaiKhoan;


                KhachHang kh = db.KhachHang.Find((Session["KhachHang"] as KhachHang).MaKH);
                log.HanhDong =  "Đã log out ";
                db.Log.Add(log);
                db.SaveChanges();
                ChiTietSachController.listCTHD = new List<Sach>();
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
          
        }
    }
}