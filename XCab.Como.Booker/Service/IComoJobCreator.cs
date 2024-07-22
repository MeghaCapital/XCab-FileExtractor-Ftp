using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xcab.como.booker.Data;
using xcab.como.booker.Data.Response;
using xcab.como.booker.Data.Variable;
using xcab.como.common;
using XCab.Como.Booker.Data;

namespace xcab.como.booker.Service
{
    public interface IComoJobCreator
    {
        Task<XcabJobResponse> Create(ComoBookingRequest payload, EBookingPhaseRequest bpRequest);
		Task<XcabJobResponse> GetQuote(ComoQuoteRequest quoteRequest, EBookingPhaseRequest bpRequest);
	}
}
