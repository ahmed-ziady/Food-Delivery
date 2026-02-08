# Domain Models

## Menu
```csharp
public class Menu
{
Menu Create ();
void AddDinner (Dinner dinner);
void RemoveDinner (Dinner dinner);
void UpdateSection (Section section);
void RemoveSection (Section section);

}

```

```{  
"id": "00000000-000-0000-0000000",
"menuName": "Menu Name",
"description": "Menu Description",
"averageRating": 4.5,
"section": "[ "id" : "000000--0000000-000",

"name": "Section Name",
"description": "Section Description",
"items": [
  {
	"id": "00000000-0000-0000-0000-00000000000",
	"itemName": "Item Name",
	"description": "Item Description",
	"price": 9.99
  },
  {
	"id": "00000000-0000-0000-0000-00000000000",
	"itemName": "Item Name",
	"description": "Item Description",
	"price": 9.99
  }
]",
"createdDateTime": "2024-06-01T12:00:00Z",
"UpdatedDateTime": "2024-06-01T12:00:00Z",
  "hostId: "00000-0000-0000-000-000000",//something like that
  "dinnerIds": [
	"00000000-0000-0000-0000-00000000000",
	"00000000-0000-0000-0000-00000000000"
  ]
  menuReviewIds: [
  "00000000-0000-0000-0000-00000000000",
}
```