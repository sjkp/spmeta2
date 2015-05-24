﻿using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPMeta2.Containers.Assertion;
using SPMeta2.Definitions;
using SPMeta2.SSOM.DefaultSyntax;
using SPMeta2.SSOM.ModelHandlers;
using SPMeta2.SSOM.ModelHosts;
using SPMeta2.Utils;
using System.Linq;
using SPMeta2.Enumerations;

namespace SPMeta2.Regression.SSOM.Validation
{
    public class ListFieldLinkDefinitionValidator : ListFieldLinkModelHandler
    {
        public override void DeployModel(object modelHost, DefinitionBase model)
        {
            var listModelHost = modelHost.WithAssertAndCast<ListModelHost>("modelHost", value => value.RequireNotNull());
            var definition = model.WithAssertAndCast<ListFieldLinkDefinition>("model", value => value.RequireNotNull());

            var list = listModelHost.HostList;
            var spObject = list.Fields[definition.FieldId];

            var assert = ServiceFactory.AssertService
                                      .NewAssert(definition, spObject)
                                            .ShouldBeEqual(m => m.FieldId, o => o.Id);

            if (!string.IsNullOrEmpty(definition.DisplayName))
                assert.ShouldBeEqual(m => m.DisplayName, o => o.Title);
            else
                assert.SkipProperty(m => m.DisplayName, "DisplayName is null or empty. Skipping");

            if (definition.Required.HasValue)
                assert.ShouldBeEqual(m => m.Required, o => o.Required);
            else
                assert.SkipProperty(m => m.Required, "Required is null or empty. Skipping");

            if (definition.Hidden.HasValue)
                assert.ShouldBeEqual(m => m.Hidden, o => o.Hidden);
            else
                assert.SkipProperty(m => m.Hidden, "Hidden is null or empty. Skipping");

            if (definition.AddFieldOptions.HasFlag(BuiltInAddFieldOptions.DefaultValue))
            {
                assert.SkipProperty(m => m.AddFieldOptions, "BuiltInAddFieldOptions.DefaultValue. Skipping.");
            }

            if (definition.AddFieldOptions.HasFlag(BuiltInAddFieldOptions.AddToAllContentTypes))
            {
                assert.ShouldBeEqual((p, s, d) =>
                {
                    var srcProp = s.GetExpressionValue(m => m.AddFieldOptions);
                    var isValid = true;

                    var listContentTypes = list.ContentTypes;

                    foreach (SPContentType ct in listContentTypes)
                    {
                        // TODO!
                        if (ct.Name == "Folder")
                        {
                            // skip folder content type
                            continue;
                        }

                        isValid = ct.FieldLinks.OfType<SPFieldLink>().Count(l => l.Id == s.FieldId) > 0;

                        if (!isValid)
                            break;
                    }

                    return new PropertyValidationResult
                    {
                        Tag = p.Tag,
                        Src = srcProp,
                        Dst = null,
                        IsValid = isValid
                    };
                });
            }

            if (definition.AddFieldOptions.HasFlag(BuiltInAddFieldOptions.AddFieldToDefaultView))
            {
                assert.ShouldBeEqual((p, s, d) =>
                {
                    var srcProp = s.GetExpressionValue(m => m.AddFieldOptions);
                    var isValid = false;

                    return new PropertyValidationResult
                    {
                        Tag = p.Tag,
                        Src = srcProp,
                        Dst = null,
                        IsValid = isValid
                    };
                });
            }

            if (definition.AddToDefaultView)
            {
                assert.ShouldBeEqual((p, s, d) =>
                {
                    var srcProp = s.GetExpressionValue(m => m.AddToDefaultView);
                    var field = list.Fields[definition.FieldId];

                    var isValid = list.DefaultView
                        .ViewFields
                        .ToStringCollection()
                        .Contains(field.InternalName);

                    return new PropertyValidationResult
                    {
                        Tag = p.Tag,
                        Src = srcProp,
                        Dst = null,
                        IsValid = isValid
                    };
                });
            }
            else
            {
                assert.SkipProperty(m => m.AddToDefaultView, "AddToDefaultView is false. Skipping.");
            }

        }
    }
}
