using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SPMeta2.Definitions.Webparts;
using SPMeta2.Models;
using SPMeta2.Standard.Definitions.Webparts;
using SPMeta2.Syntax.Default;

namespace SPMeta2.Standard.Syntax
{

    [Serializable]
    [DataContract]
    public class CommunityAdminWebPartModelNode : WebPartModelNode
    {

    }

    public static class CommunityAdminWebPartDefinitionSyntax
    {
        #region methods

        public static TModelNode AddCommunityAdminWebPart<TModelNode>(this TModelNode model, CommunityAdminWebPartDefinition definition)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return AddCommunityAdminWebPart(model, definition, null);
        }

        public static TModelNode AddCommunityAdminWebPart<TModelNode>(this TModelNode model, CommunityAdminWebPartDefinition definition,
            Action<CommunityAdminWebPartModelNode> action)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return model.AddTypedDefinitionNode(definition, action);
        }

        #endregion

        #region array overload

        public static TModelNode AddCommunityAdminWebParts<TModelNode>(this TModelNode model, IEnumerable<CommunityAdminWebPartDefinition> definitions)
           where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            foreach (var definition in definitions)
                model.AddDefinitionNode(definition);

            return model;
        }

        #endregion
    }
}
