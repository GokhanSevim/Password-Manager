using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Results.Concrete.Results;
using NLog;

namespace Filters
{
	public class TryExceptionAttribute : ExceptionFilterAttribute
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public override void OnException(ExceptionContext context)
		{
			try
			{
				var response = new Result(false, context.Exception.Message);

				context.Result = new BadRequestObjectResult(response);

				context.ExceptionHandled = true;

				_logger.Error(context.Exception);
			}
			catch (Exception ex)
			{
				context.Result = new BadRequestObjectResult(ex);

				_logger.Error(ex);
			}
		}
	}
}
