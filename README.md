# Sitecore SXA Delegated Area NonInheritedFields
Adds NonInheritedFields setting support for SXA Delegated Area clone pushing feature. Which means if a field is set up in here as a nonInherited field, when the field value changes in the original item, the field value on the clone item is not reset.

Note that this repo is a similar feature to "ItemCloning.NonInheritedFields" setting for standard clones but this repo applies to SXA Delegated area feature only.

## Versions
It was tested against Sitecore 9.1.1 and SXA 1.8.1. Should work with other versions, however, have not tested.

## Usages
1. Check out the repo, restore nuget and build the solution
2. Set field ids or names in setting "XA.Foundation.Multisite.DelegatedArea.NonInheritedFields" in \XA.Foundation.Multisite.Custom\App_Config\Include\z.Foundation.Overrides\XA.Foundation.Multisite.CustomResetFields.config
3. Deploy the solution to Sitecore instance

## Important
This is POC code; please apply, test before use in a production environment.
