
<h1>EntityDataFramework</h1>

code:
```
class Contact {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ContactType ContactType { get; set; }
}
class ContactType {
    public Guid Id { get; set; }
    public string Name { get; set; }
}

var path = Path.Combine(Environment.CurrentDirectory, "contact.db");
var engine = new SQLiteDbEngine(path);
var select = new SelectQuery<Contact>(engine);
select.AddColumn(contact => contact.Id, "RecordId");
select.AddColumn(contact => contact.Name);
select.AddColumn(contact => contact.ContactType.Id);
select.AddColumn(contact => contact.ContactType.Name);
select.AddColumn(contact => contact.ContactType.Name, "TypeName");
select.AddWhere(contact => contact.ContactType.Name == "Type1" || contact.Name == "Contact1");
var entities = select.GetEntities();
var sqlText = select.GetSelectQuerySqlText(engine.GetSelectQuerySqlBuilder());
```
entities:
```
[
  {
    "Values": {
      "RecordId": "681b5c52-18f2-42db-9fe4-2c967bb0b68f",
      "ContactName": "Contact1",
      "ContactContactTypeId": "66aaab6d-3aa0-42a1-9a1b-8e0fd9ad657c",
      "ContactTypeName": "Type1",
      "TypeName": "Type1"
    }
  },
  {
    "Values": {
      "RecordId": "45dd64c7-5795-48f1-bff3-116c0f7d3522",
      "ContactName": "Contact3",
      "ContactContactTypeId": "66aaab6d-3aa0-42a1-9a1b-8e0fd9ad657c",
      "ContactTypeName": "Type1",
      "TypeName": "Type1"
    }
  }
]
```
sqlText:
```
SELECT "Contact"."Id" AS RecordId, "Contact"."Name" AS ContactName, "Contact"."ContactTypeId" AS ContactContactTypeId, "ContactType"."Name" AS ContactTypeName, "ContactType"."Name" AS TypeName FROM "Contact"INNER JOIN "ContactType" AS "ContactType" ON "ContactType"."Id" = "Contact"."ContactTypeId"
WHERE (("ContactType"."Name" = 'Type1' OR "Contact"."Name" = 'Contact1'))
```

code:
```
var path = Path.Combine(Environment.CurrentDirectory, "contact.db");
var engine = new SQLiteDbEngine(path);
var select = new SelectQuery<ContactType>(engine);
select.AddAggregatedColumn(contactType => contactType.Id, QueryAggregationFunctionType.Count, "Count");
var entities = select.GetEntities();
var entity = entities.First();
var count = entity.GetValue<int>("Count");
```
sql:
```
SELECT Count("ContactType"."Id") AS Count FROM "ContactType"
```
