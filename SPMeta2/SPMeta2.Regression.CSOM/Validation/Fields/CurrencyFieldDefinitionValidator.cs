﻿using SPMeta2.CSOM.ModelHandlers.Fields;
using SPMeta2.CSOM.ModelHosts;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Base;
using SPMeta2.Definitions.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SPMeta2.Utils;
using Microsoft.SharePoint.Client;
using SPMeta2.Exceptions;
using System.Xml.Linq;

namespace SPMeta2.Regression.CSOM.Validation.Fields
{
    public class CurrencyFieldDefinitionValidator : CurrencyFieldModelHandler
    {
        public override void DeployModel(object modelHost, DefinitionBase model)
        {
            var siteModelHost = modelHost.WithAssertAndCast<SiteModelHost>("modelHost", value => value.RequireNotNull());
            var definition = model.WithAssertAndCast<CurrencyFieldDefinition>("model", value => value.RequireNotNull());

            var spObject = FindField(modelHost, definition);

            var assert = ServiceFactory.AssertService.NewAssert(model, definition, spObject);

            assert
                .ShouldBeEqual(m => m.Title, o => o.Title)
                    .ShouldBeEqual(m => m.InternalName, o => o.InternalName)
                    .ShouldBeEqual(m => m.Id, o => o.Id)
                    .ShouldBeEqual(m => m.Required, o => o.Required)
                    .ShouldBeEqual(m => m.Description, o => o.Description)
                    .ShouldBeEqual(m => m.FieldType, o => o.TypeAsString)
                    .ShouldBeEqual(m => m.Group, o => o.Group);

            // TODO
        }
    }
}
