using System;
using SPMeta2.CSOM.ModelHandlers;
using SPMeta2.CSOM.ModelHosts;
using SPMeta2.Definitions.Base;
using SPMeta2.Enumerations;
using SPMeta2.Standard.Definitions.Webparts;
using SPMeta2.Utils;

namespace SPMeta2.CSOM.Standard.ModelHandlers.Webparts
{
    public class SearchNavigationWebPartModelHandler : WebPartModelHandler
    {
        public SearchNavigationWebPartModelHandler()
        {
            ShouldUseWebPartStoreKeyForWikiPage = true;
        }

        #region properties

        public override Type TargetType
        {
            get { return typeof(SearchNavigationWebPartDefinition); }
        }

        #endregion

        #region methods

        protected override string GetWebpartXmlDefinition(ListItemModelHost listItemModelHost, WebPartDefinitionBase webPartModel)
        {
            var wpModel = webPartModel.WithAssertAndCast<SearchNavigationWebPartDefinition>("model", value => value.RequireNotNull());
            var wpXml = WebpartXmlExtensions
                .LoadWebpartXmlDocument(BuiltInWebPartTemplates.SearchNavigationWebPart);
                
            // TODO, process XML

            return wpXml.ToString();
        }

        #endregion
    }
}
