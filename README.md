# FSL.CsharpUsefulExtensionsMethods

**C# Usefull Extensions Methods**

Extension Methods are great C# feature and I will share some of my own extension methods that will help you in your applications. This repository will be constantly updated.

![enter image description here](https://fabiosilvalima.files.wordpress.com/2016/12/csharp-extension.png)

What is in the source code?
---

#### <i class="icon-file"></i> FSL.CsharpUsefulExtensionsMethods

- Visual Studio solution file;
- Class project with Extensions; 
- *FSLBrowserCapabilitiesExtension* - Extensions for check browser version;
- *FSLCollectionExtension* - Extensions to work with collections;
- *FSLEnumExtension* - Extensions for Enum;
- *FSLIsNullExtension* - Extensions to check nullable objects;
- *FSLQueryStringExtension* - Extensions to work with query string;
- *FSLSerializationExtension* - Extensions to serialize objects;
- *FSLStringExtension* - Extensions for string type;

---

**Usage samples**

```csharp
var name = someVariableName.IsNull("fabio"); //if someVariableName is null returns "fabio"

var obj = someObject.IsNull(); //if someObject is null returns a new instance of someObject

var strEnum = "September".ToEnum<Months>(); //will convert "September" string to Months Enum

var qs = Request.QueryString.GetSecure("culture").IsNull("en-US"); //will return the value of querystring "culture" in secure mode. If is null, returns "en-US" as default

var xml = someObject.ToXml(); //serializes someObject to XML;

var xml = someObject.ToJson(); //serializes someObject to Json;

var instance = someList.FirstOrNew(); //return the first item of collection, if there is no item, returns a new instance;
```



References:
---

- More articles and code [check here][1];

Licence:
---

- Licence MIT


[1]: https://fabiosilvalima.net
