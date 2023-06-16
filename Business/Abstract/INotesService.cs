using Dto.Crypt;
using Entities;
using Results.Abstract.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface INotesService
	{
		Task<IDataResult<NoteDto>> GetAsync(Expression<Func<ENotes, bool>> filter);

		Task<IDataResult<List<NoteDto>>> FindAllAsync(Expression<Func<ENotes, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<ENotes>, IOrderedQueryable<ENotes>> orderBy = null);

		Task<IDataResult<NoteDto>> AddAsync(NoteDto input);

		Task<IDataResult<NoteDto>> UpdateAsync(NoteDto input);

		Task<IResult> DeleteAsync(Expression<Func<ENotes, bool>> filter);

		Task<IResult> DeleteAllAsync();
	}
}
