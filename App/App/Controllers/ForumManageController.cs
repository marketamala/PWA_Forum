using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Http.Results;

namespace App.Controllers
{
    public class ForumManageController : Controller
    {
        public static int page = 1;
        public static Themes actTheme;
        public static Posts actPost;

        public ActionResult Themes()
        {
            page = 1;
            return View();
        }

        public ActionResult _LoadThemes(string search)
        {
            var themes = new ThemesController().GetThemes();
            if (search != null)
                themes = themes.Where(t => t.ThemeName.Contains(search));
            return PartialView(themes.ToList());
        }

        public ActionResult NewTheme()
        {
            if (Session["UserID"] != null)
            {
                ModelState.Clear();
                Themes th = new Themes();
                th.CreatedBy = Session["UserName"].ToString();
                return View(th);
            }
            else return RedirectToAction("Login", "AccountManage");
        }

        [HttpPost]
        public ActionResult NewTheme(Themes theme)
        {
            if (ModelState.IsValid)
            {
                theme.CreatedAt = DateTime.Now;
                var r = new ThemesController().PostThemes(theme);
                return RedirectToAction("Themes");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nové téma nebylo vytvořeno!");
                return View();
            }
        }

        public ActionResult GoTheme(Themes theme)
        {
            if ((actTheme == null)||(theme.ThemeID != actTheme.ThemeID))
                actTheme = theme;
            Session["ThemeName"] = theme.ThemeName;
            actPost = null;
            return View(actTheme);
        }

        public ActionResult _GetPosts()
        {
            if (actTheme != null)
            {
                var posts = new PostsController().GetPosts().Where(p => p.ThemeID == actTheme.ThemeID).OrderByDescending(p => p.AddedAt);
                return PartialView(posts.ToPagedList(page, 8));
            }
            else return RedirectToAction("Themes");
        }

        public ActionResult _AddPost()
        {
            if (actTheme != null)
            {
                Posts newPost = new Posts();
                newPost.ThemeID = actTheme.ThemeID;
                newPost.AddedBy = Session["UserName"].ToString();
                return PartialView(newPost);
            }
            else return RedirectToAction("Themes");
        }

        [HttpPost]
        public ActionResult _AddPost(Posts post)
        {
            if ((ModelState.IsValid) && (post.Content != null))
            {
                post.AddedAt = DateTime.Now;
                post.AddedBy = Session["UserName"].ToString();
                var r = new PostsController().PostPosts(post);
                page = 1;
                ModelState.Clear();
                post.Content = null;
                return PartialView(post);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Příspěvek nebyl přidán!");
                return PartialView(post);
            }
        }

        public ActionResult GoPost(Posts post)
        {
            actPost = post;
            return View(post);
        }

        public ActionResult _GetResponses(Posts post)
        {
            var responses = new ResponsesController().GetResponses().Where(r => r.RootID == post.PostID).OrderBy(r => r.AddedAt);
            return PartialView(responses.ToList());
        }

        public ActionResult _AddResponse(Posts post)
        {
            Responses response = new Responses();
            response.RootID = post.PostID;
            response.AddedBy = Session["UserName"].ToString();
            return PartialView(response);
        }

        [HttpPost]
        public ActionResult _AddResponse(Responses response)
        {
            if ((ModelState.IsValid) && (response.RContent != null))
            {
                response.AddedAt = DateTime.Now;
                response.AddedBy = Session["UserName"].ToString();
                var r = new ResponsesController().PostResponses(response);
                ModelState.Clear();
                response.RContent = null;
                return PartialView(response);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Odpověď nebyla přidána!");
                return PartialView(response);
            }
        }

        public ActionResult DelTheme(Themes theme)
        {
            var r = new ThemesController().DeleteThemes(theme.ThemeID);
            return RedirectToAction("Themes");
        }

        public ActionResult DelPost(Posts post)
        {
            var r = new PostsController().DeletePosts(post.PostID);
            return RedirectToAction("GoTheme", actTheme);
        }

        public ActionResult DelResponse(Responses response)
        {
            var r = new ResponsesController().DeleteResponses(response.ResponseID);
            if (actPost != null)
                return RedirectToAction("GoPost", actPost);
            else
                return RedirectToAction("GoTheme", actTheme);
        }

        public ActionResult SwitchPage(int switchedPage)
        {
            if (actTheme != null)
            {
                page = switchedPage;
                return RedirectToAction("GoTheme", actTheme);
            }
            else return RedirectToAction("Themes");
        }

        public ActionResult BackToTheme()
        {
            return RedirectToAction("GoTheme", actTheme);
        }
    }
}