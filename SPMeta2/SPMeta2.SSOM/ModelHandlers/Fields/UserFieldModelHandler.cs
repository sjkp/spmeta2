﻿using System;
using System.Xml.Linq;
using Microsoft.SharePoint;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Fields;
using SPMeta2.Enumerations;
using SPMeta2.Utils;

namespace SPMeta2.SSOM.ModelHandlers.Fields
{
    public class UserFieldModelHandler : FieldModelHandler
    {
        #region properties

        public override Type TargetType
        {
            get { return typeof(UserFieldDefinition); }
        }

        protected override Type GetTargetFieldType(FieldDefinition model)
        {
            return typeof(SPFieldUser);
        }

        #endregion

        #region methods

        protected override void ProcessFieldProperties(SPField field, FieldDefinition fieldModel)
        {
            // let base setting be setup
            base.ProcessFieldProperties(field, fieldModel);
        }

        protected override void ProcessSPFieldXElement(XElement fieldTemplate, FieldDefinition fieldModel)
        {
            base.ProcessSPFieldXElement(fieldTemplate, fieldModel);

            var typedFieldModel = fieldModel.WithAssertAndCast<UserFieldDefinition>("model", value => value.RequireNotNull());

            fieldTemplate.SetAttribute(BuiltInFieldAttributes.AllowMultipleValues, typedFieldModel.AllowMultipleValues.ToString().ToUpper());
            fieldTemplate.SetAttribute(BuiltInFieldAttributes.AllowDisplay, typedFieldModel.AllowDisplay.ToString().ToUpper());
            fieldTemplate.SetAttribute(BuiltInFieldAttributes.Presence, typedFieldModel.Presence.ToString().ToUpper());

            if (typedFieldModel.SelectionGroup.HasValue)
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.SelectionGroup, typedFieldModel.SelectionGroup.ToString());

            if (!string.IsNullOrEmpty(typedFieldModel.SelectionMode))
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.SelectionMode, typedFieldModel.SelectionMode);
        }

        #endregion
    }
}
