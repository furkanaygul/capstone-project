using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitirmeProV3.Models;
using BitirmeProV3.Models.Entity;

namespace BitirmeProV3.Controllers
{
    public class CardPageController : Controller
    {
        // GET: CardPage
        BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
      
        public ActionResult CardPage(int id)
        {
            var model = new CardPageModel();
            var sorgu = db.TBL_IndexKart;
            
            model.yorumlar = new List<Yorumlar>();
            foreach (var item in sorgu)
            {
                if (id==item.IndexKartID)
                {
                    foreach (var i in db.TBL_IndexCardComment)
                    {
                        if (i.IndexKartID==id)
                        {
                            var yorum = new Yorumlar();
                            yorum.yorum = i;
                            foreach (var user in db.TBL_Kullanici)
                            {
                                if (i.KullaniciAdi==user.TBL_KullaniciOzellik.KullaniciAdi)
                                {
                                    yorum.kullanici = user;

                                    break;
                                }
                            }
                            model.yorumlar.Add(yorum);
                        }

                    }
                    model.Card = item;
                    return View(model);
                    
                }
            }
            
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(CardPageModel model,string username,int cardID) {

            BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
            if (model.yorum.Comment.Length<1000)
            {
                model.yorum.IndexKartID = cardID;
                model.yorum.KullaniciAdi = username;
                model.yorum.Tarih = DateTime.Now;
                db.TBL_IndexCardComment.Add(model.yorum);
                db.SaveChanges();
            }
            return RedirectToAction("CardPage","CardPage", new {id= cardID });
        }

        public ActionResult CommentDelete(int yorumId,int id)
        {
            var sorgu = db.TBL_IndexCardComment.ToList();
            foreach (var item in sorgu)
            {
                if (item.CardCommentID==yorumId)
                {
                    db.TBL_IndexCardComment.Remove(item);
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("CardPage", "CardPage",new {id=id });

        }
    }
}