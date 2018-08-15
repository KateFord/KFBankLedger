using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KFBankLedger.Models;

namespace KFBankLedger.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transaction
        public async Task<ActionResult> Index(Guid? id)
        {
            if (id != null) Session["bankAccountId"] = id.ToString();
             var sessionBankAccountId = Session["bankAccountId"];
            var transactions = db.Transactions.Where(u => u.BankAccount.BankAccountId == id);
            return View(await transactions.ToListAsync());
        }


        // GET: Transaction/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "transactionDateCreated,transactionType,transactionAmount,transactionDescription")] Transaction transaction)
        {

            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    // UP TO HERE ... bankAccountID is null
                    transaction.TransactionId = Guid.NewGuid();
                    Guid bankAccountId = Guid.Parse(Session["bankAccountId"].ToString()); 
                    transaction.BankAccount = db.BankAccounts.Single(b => b.BankAccountId == bankAccountId);
                     db.Transactions.Add(transaction);
                    await db.SaveChangesAsync();
                }
            }
            catch (DataException ex)
            {
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem perists, see your system administrator.");
                ModelState.AddModelError("", ex.InnerException);
            }

            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "transactionDateCreated,transactionType,transactionAmount,transactionDescription")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            db.Transactions.Remove(transaction);
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
