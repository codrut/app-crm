﻿// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using XamarinCRM.Models;
using XamarinCRMAppService.Models;

namespace XamarinCRMAppService.Controllers
{
    [MobileAppController]
    public abstract class BaseController<T> : TableController<T> where T : BaseModel
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<T>(context, Request);
        }

        // GET tables/Account
        public IQueryable<T> GetAll()
        {
#if TRY_APP_SERVICE
            // In your production mobile service, do not evaluate the query by calling ToList() before returning 
            // the results. This is required due to SQL Compact Edition not supporting the DateTimeOffset type. 
            return Query().ToList().AsQueryable();
#else
            return Query();
#endif
        }

        public T Get(string id)
        {
            return Lookup(id).Queryable.FirstOrDefault();
        }
    }
}
