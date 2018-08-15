using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using KFBankLedger.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Data;

namespace KFBankLedger.Controllers
{
    public class BankAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccount
        public async Task<ActionResult> Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return Redirect("Home/Index");
            }
             else
            {
                var applicationUserId = User.Identity.GetUserId();
                var bankAccounts = db.BankAccounts.Where(x => x.User.Id == applicationUserId);
                return View(await bankAccounts.ToListAsync());
            }
        }

        // GET: BankAccount/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "bankAccountName,bankAccountNumber,bankAccountDateCreated,bankAccountType,bankAccountBalance")] BankAccount bankAccount)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    bankAccount.BankAccountId = Guid.NewGuid();
                    bankAccount.BankAccountDateCreated = DateTime.Today;
                    bankAccount.BankAccountBalance = 0;
                    var applicationUserId = User.Identity.GetUserId();
                    bankAccount.User = db.Users.Single(u => u.Id == applicationUserId);
                    db.BankAccounts.Add(bankAccount);
                    await db.SaveChangesAsync();
                }

            }
            catch (DataException ex)
            {
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem perists, see your system administrator.");
                ModelState.AddModelError("", ex.InnerException);
            }
 
             return View(bankAccount);
        }

        // GET: BankAccount/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "bankAccountName,bankAccountNumber,bankAccountDateCreated,bankAccountType,bankAccountBalance")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bankAccount);
        }

        // GET: BankAccount/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            db.BankAccounts.Remove(bankAccount);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
