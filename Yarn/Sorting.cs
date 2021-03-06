﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yarn.Extensions;

namespace Yarn
{
    public class Sorting<T>
    {
        private Lazy<Expression<Func<T, object>>> _orderBy;
        private Lazy<string> _path;

        public Expression<Func<T, object>> OrderBy
        {
            get { return _orderBy.Value; }
            set
            {
                _orderBy = new Lazy<Expression<Func<T,object>>>(() => value);
                _path = new Lazy<string>(() => value == null ? null : value.Body.ToString());
            }
        }

        public string Path
        {
            get { return _path.Value; }
            set
            {
                _path = new Lazy<string>(() => value);
                _orderBy = new Lazy<Expression<Func<T, object>>>(() => value == null ? null : typeof(T).BuildLambdaExpression(value) as Expression<Func<T, object>>);
            }
        }
        
        public bool Reverse { get; set; }
    }
}
