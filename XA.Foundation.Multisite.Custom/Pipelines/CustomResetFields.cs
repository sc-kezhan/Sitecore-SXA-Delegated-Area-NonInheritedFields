using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Multisite.Pipelines.PushCloneChanges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XA.Foundation.Multisite.Custom.Pipelines.PushCloneChanges
{
    public class CustomResetFields : PushCloneChangesProcessorBase
    {
        BaseSettings _settings;

        public CustomResetFields(BaseSettings settings)
        {
            _settings = settings;
        }

        public IReadOnlyCollection<string> NonInheritedFields => _settings.GetPipedListSetting(Constants.DelegatedAreaNonInheritedFieldsSettingName, Array.Empty<string>());

        public override void Process(PushCloneChangesArgs args)
        {
            Func<FieldChange, string, bool> checkNonInheritedFieldKey = delegate (FieldChange fieldChange, string fieldIdOrName)
            {
                if (ID.TryParse(fieldIdOrName, out ID result))
                {
                    return fieldChange.FieldID == result;
                }
                return fieldChange.Definition != null && !string.IsNullOrEmpty(fieldChange.Definition.Key) && fieldChange.Definition.Key.ToUpperInvariant() == fieldIdOrName.ToUpperInvariant();
            };

            foreach (FieldChange fieldChange in args.Changes.FieldChanges)
            {
                if (!NonInheritedFields.Any(fieldIdOrName => checkNonInheritedFieldKey(fieldChange, fieldIdOrName)))
                {
                    Field field = args.Clone.Fields[fieldChange.FieldID];
                    args.Clone.Editing.BeginEdit();
                    field.Reset();
                    args.Clone.Editing.EndEdit();
                }
            }
        }
    }
}