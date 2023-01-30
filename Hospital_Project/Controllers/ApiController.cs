using DataRepository.Contract;
using DataRepository.Model;
using Hospital_Project.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_Project.Controllers
{
    [Route("api/hospital_project")]
    public class ApiController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public ApiController(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult GetDetails()
        {
            var getData = _dataRepository.GetAllDetails();
            var LimitedData = getData.Select(ById => new RestrictedModel
            {
                Id=ById.Id,
                First_Name=ById.First_Name,
                Last_Name=ById.Last_Name,
                Gender=ById.Gender,
                Email=ById.Email,
                Phone_Number=ById.Phone_Number,
                Res_Address=ById.Res_Address
            });

            return Ok(LimitedData);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var dataById = _dataRepository.GetDataById(id);
                return Ok(dataById);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertEmployee([FromBody] DetailedModel detailedModel)
        {
            try
            {

                if(detailedModel.User_Password == detailedModel.Confirm_Password)
                {
                    _dataRepository.InsertDetails(MapDetailedModelToDataModel(detailedModel));
                    return StatusCode(StatusCodes.Status201Created,"Password Matched\nUser Created Successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "Password doesnot match!");
                }

                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private DataModel MapDetailedModelToDataModel(DetailedModel detailedModel)
        {
            var dataModel = new DataModel
            {
                First_Name = detailedModel.First_Name,
                Last_Name = detailedModel.Last_Name,
                Gender = detailedModel.Gender,
                Email = detailedModel.Email,
                Phone_Number = detailedModel.Phone_Number,
                Res_Address = detailedModel.Res_Address,
                User_Password = detailedModel.User_Password             
            };
            return dataModel;
        }
    }
}
