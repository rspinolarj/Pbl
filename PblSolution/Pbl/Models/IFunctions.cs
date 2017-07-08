using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pbl.Models
{
    interface IFunctions<T>
    {
        bool Add(T t);
        bool Update(T t);
        bool Delete(T t);
        List<T> Bring(Expression<Func<T, bool>> predicate);
        T BringOne(Expression<Func<T, bool>> predicate);
        List<T> BringAll();
    }
}
