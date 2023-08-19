using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;

namespace Online_Exam_System.Controllers
{
    public class BatchController : Controller
    {
        private readonly ILogger<BatchController> _logger;
        private readonly OnlineExamSystemContext context;

        public BatchController(ILogger<BatchController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult GetAllBatch()
        {
            var data = context.Batches.Include(x => x.Department).ToList(); ///lambda expression
            return View(data);
        }

        //create Batch
        public IActionResult AddBatch()
        {
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return View();
        }
        //create Batch
        [HttpPost]
        public IActionResult AddBatch(Batch batch)
        {
            context.Batches.Add(batch);
            context.SaveChanges();
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;

            return RedirectToAction("GetAllbatch", "Batch");
        }

        //Update or Edit Batch
        public IActionResult UpdateBatchDetails(int id)
        {
            Batch aBatch = context.Batches.FirstOrDefault(x => x.BatchId == id);
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return View(aBatch);
        }

        [HttpPost]
        public IActionResult UpdateBatchDetails(int id, Batch batch)
        {
            var aBatch = context.Batches.Find(id);

            aBatch.BatchName = batch.BatchName;
            context.Batches.Update(aBatch);
            context.SaveChanges();
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return RedirectToAction("GetAllBatch", "Batch");
        }

        //delete Batch
        public IActionResult DeleteBatch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteBatch(int newId)
        {
            var aBatch = context.Batches.Find(newId);
            context.Batches.Remove(aBatch);
            context.SaveChanges();
            return RedirectToAction("GetAllBatch", "Batch");
        }
    }
}
