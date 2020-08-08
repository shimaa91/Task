using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Services.GeneralServices
{
    public  class GeneralService:IGeneralService
    {
        private readonly IMapper _mapper;
        public GeneralService(IMapper mapper)
        {
            _mapper = mapper;            
        }
    }
}
