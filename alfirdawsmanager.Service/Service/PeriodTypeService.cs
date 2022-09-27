using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class PeriodTypeService : IPeriodTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public PeriodTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the period types within the platform
        /// </summary>
        /// <returns>List of period types</returns>
        public Task<List<PeriodTypeModel>> GetPeriodTypesOverview()
        {
            try
            {
                List<PeriodTypeModel> periodTypes = _context.PeriodTypes
                                                .Select(c => new PeriodTypeModel
                                                {
                                                    Name = c.Name,
                                                    NrOfDays = c.NrOfDays,
                                                    PeriodTypeId = c.PeriodTypeId
                                                }).ToList();

                return Task.FromResult(periodTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new period type 
        /// </summary>
        /// <param name="periodTypeRequest">The period type request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreatePeriodType(PeriodTypeCreateRequest periodTypeRequest)
        {
            try
            {
                Response response = new Response();

                var objPeriodType = new PeriodType();
                objPeriodType.Name = periodTypeRequest.Name;
                objPeriodType.NrOfDays = periodTypeRequest.NrOfDays;

                var existPeriodType = _context.PeriodTypes.FirstOrDefault(a => a.Name.Equals(periodTypeRequest.Name));
                if (existPeriodType == null)
                {
                    _context.Add(objPeriodType);
                    _context.SaveChanges();
                    response.Data = objPeriodType;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Period Type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updates a period type
        /// </summary>
        /// <param name="periodTypeRequest">The period type update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdatePeriodType(PeriodTypeUpdateRequest periodTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existPeriodType = _context.PeriodTypes.FirstOrDefault(a => a.Name.Equals(periodTypeRequest.Name) && a.PeriodTypeId != periodTypeRequest.PeriodTypeId);
                if (existPeriodType == null)
                {
                    var objPeriodType = _context.PeriodTypes.FirstOrDefault(a => a.PeriodTypeId == periodTypeRequest.PeriodTypeId);
                    if (objPeriodType != null)
                    {
                        if (objPeriodType.Name != null) objPeriodType.Name = periodTypeRequest.Name;
                        if (objPeriodType.NrOfDays != null) objPeriodType.NrOfDays = periodTypeRequest.NrOfDays;
                        _context.Update(objPeriodType);
                        _context.SaveChanges();
                        response.Data = objPeriodType;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Period Type with same name already exist.";
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a period type
        /// </summary>
        /// <param name="periodTypeId">The unique id of the period type that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeletePeriodType(int periodTypeId)
        {
            try
            {
                bool success = false;
                var objPeriodType = _context.PeriodTypes.FirstOrDefault(a => a.PeriodTypeId == periodTypeId);
                if (objPeriodType != null)
                {
                    _context.Remove(objPeriodType);
                    _context.SaveChanges();
                    success = true;
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific period type 
        /// </summary>
        /// <param name="periodTypeId">The unique id of the period type</param>
        /// <returns>The period type object</returns>
        public Task<PeriodTypeModel> GetPeriodTypeById(int periodTypeId)
        {
            try
            {
                PeriodTypeModel periodTypeModel = new PeriodTypeModel();
                var periodType = _context.PeriodTypes.FirstOrDefault(s => s.PeriodTypeId == periodTypeId);
                if (periodType != null)
                {
                    periodTypeModel = _mapper.Map<PeriodTypeModel>(periodType);
                }

                return Task.FromResult(periodTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

