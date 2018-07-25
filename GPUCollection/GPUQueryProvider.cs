﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;

namespace GPUCollection
{
    /// <summary>
    /// Boilerplate code from https://blogs.msdn.microsoft.com/mattwar/2007/07/31/linq-building-an-iqueryable-provider-part-ii/
    /// </summary>
    public class GPUQueryProvider<T> : BaseQueryProvider
    {
        private IEnumerable<T> data;

        public GPUQueryProvider(IEnumerable<T> data)
        {
            this.data = data;
        }

        public override string GetQueryText(Expression expression)
        {
            return this.Translate(expression);
        }

        public override object Execute(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            this.Translate(expression); // Call translate to compile the expression to LLVM
            // Call something to take the LLVM module and run it through the rest of the pipeline
            // Get the results of the pipeline
            return new int[] { }; // Actually return a useful IEnumerable later
            /*
            return Activator.CreateInstance(
                typeof(ObjectReader<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.NonPublic, null,
                new object[] { reader },
                null);
            */
        }

        private string Translate(Expression expression)
        {
            return new QueryTranslator<T>(data).Translate(expression);
        }
    }
}
