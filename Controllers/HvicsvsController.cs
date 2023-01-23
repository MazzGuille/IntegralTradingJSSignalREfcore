using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using IntegralTradingJSSignalREfcore.Models;

namespace IntegralTradingJSSignalREfcore.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HvicsvsController : Controller
    {
        private DevExtremeContext _context;

        public HvicsvsController(DevExtremeContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var hvicsvs = _context.Hvicsvs.Select(i => new {
                i.Id,
                i.Uhml,
                i.Ui,
                i.Strength,
                i.Sfi,
                i.Mic,
                i.ColorGrade,
                i.TrashId,
                i.OrderId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(hvicsvs, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Hvicsv();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Hvicsvs.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Hvicsvs.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Hvicsvs.FirstOrDefaultAsync(item => item.Id == key);

            _context.Hvicsvs.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> OrdersLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Orders
                         orderby i.CreatedAt
                         select new {
                             Value = i.OrderId,
                             Text = i.CreatedAt
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Hvicsv model, IDictionary values) {
            string ID = nameof(Hvicsv.Id);
            string UHML = nameof(Hvicsv.Uhml);
            string UI = nameof(Hvicsv.Ui);
            string STRENGTH = nameof(Hvicsv.Strength);
            string SFI = nameof(Hvicsv.Sfi);
            string MIC = nameof(Hvicsv.Mic);
            string COLOR_GRADE = nameof(Hvicsv.ColorGrade);
            string TRASH_ID = nameof(Hvicsv.TrashId);
            string ORDER_ID = nameof(Hvicsv.OrderId);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(UHML)) {
                model.Uhml = values[UHML] != null ? Convert.ToDecimal(values[UHML], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if(values.Contains(UI)) {
                model.Ui = values[UI] != null ? Convert.ToDecimal(values[UI], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if(values.Contains(STRENGTH)) {
                model.Strength = values[STRENGTH] != null ? Convert.ToDecimal(values[STRENGTH], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if(values.Contains(SFI)) {
                model.Sfi = values[SFI] != null ? Convert.ToDecimal(values[SFI], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if(values.Contains(MIC)) {
                model.Mic = values[MIC] != null ? Convert.ToDecimal(values[MIC], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if(values.Contains(COLOR_GRADE)) {
                model.ColorGrade = Convert.ToString(values[COLOR_GRADE]);
            }

            if(values.Contains(TRASH_ID)) {
                model.TrashId = values[TRASH_ID] != null ? Convert.ToDecimal(values[TRASH_ID], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if(values.Contains(ORDER_ID)) {
                model.OrderId = values[ORDER_ID] != null ? Convert.ToInt32(values[ORDER_ID]) : (int?)null;
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}