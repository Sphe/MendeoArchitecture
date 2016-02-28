using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class EntityWithValidation<T>
        where T : class
    {
        private T _entity;
        private IList<Tuple<string, string>> _errors;

        public EntityWithValidation(T entity)
        {
            _errors = new List<Tuple<string, string>>();
            _entity = entity;
        } 

        public EntityWithValidation(T entity, IList<Tuple<string, string>> errors)
        {
            _errors = errors;
            _entity = entity;
        }

        public T Entity {
            get
            {
                return _entity;
            }
        }

        public IList<Tuple<string, string>> Errors 
        { 
            get
            {
                return _errors;
            }
        }
    }
}
