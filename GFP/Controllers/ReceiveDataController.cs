using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;
using GFP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GFP.Controllers
{
    public class ReceiveDataController : ControllerBase
    {
        private readonly IReceiveDataProvider _ReceiveDataProvider;

        public ReceiveDataController(IReceiveDataProvider receiveDataProvider)
        {
            _ReceiveDataProvider = receiveDataProvider;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPrograms([FromForm]IFormFile file)
        {
            try
            {


                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<SocialProgramModel>();

                    return Ok(_ReceiveDataProvider.UploadProgramsAsync(records.ToList()));
                }


                //return Ok();
            }
            catch (MySqlException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRawSocialPrograms()
        {
            try
            {
                return Ok(await _ReceiveDataProvider.GetRawSocialPrograms());
            }
            catch (MySqlException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetElegibleSocialPrograms()
        {
            try
            {
                return Ok(await _ReceiveDataProvider.GetElegibleSocialPrograms());
            }
            catch (MySqlException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRuleSocialPrograms()
        {
            try
            {
                return Ok(await _ReceiveDataProvider.GetRuleSocialPrograms());
            }
            catch (MySqlException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRules()
        {
            try
            {
                return Ok(await _ReceiveDataProvider.GetRules());
            }
            catch (MySqlException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
