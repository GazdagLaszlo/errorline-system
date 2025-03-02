#ErrorLine-system LRT

##Entitások


User
- id
- roles | ENUM | Kollégista, Karbantartó, Adminisztrátor, Karbantartási vezetö

Issue
- id | Integer | PK 
- description | String | Hosszú részletes leírás
- facilityId | Integer - FK
- issueTypeId | Integer - FK
- item | String --- barmi lehet?
- state | ENUM | Open, Blocked, InProgress, Fixed, Verified, Closed
- internalComment | String
- parentIssueId | Integer - FK | Csak Done state után vegallapotot lehet összefüzni
- assignedId | FK, depends on the UserEntity primaryKey | pl.karbantartó
- createDateTime | Timestamp
- createdBy | String
- equipments | List<Equipment> | DBben az equipment oldalrol van relacio
- equipmentOrders | List<EquipmentOrder> | DBben az order oldalrol van relacio



EquipmentOrder
- id | Integer
- issueId | Integer | FK | nullable
- equipmentId | FK
- quantiy | Integer
- state | Enum | Open, OnGoing, Completed



Equipment
- id | Integer
- name | String
- facilityId | Integer - FK
- issueId | Integer | FK | nullable
- createDateTime | Timestamp
- createdBy | String
- inStock | Boolean
- equipmentOrders | List<EquipmentOrder> | DBben az order oldalrol van relacio



IssueType
- id
- name

 Kezdeti ertekkeszlet: {1,TechnicalError}, {2,AestheticDamage}, {3,FunctionalIssue}, {4,HygieneProblem}
 Műszaki hiba (pl. elromlott egy elektromos készülék), Esztétikai hiba (pl. sérült a b   útor vagy a fal), Funkcionális hiba (pl. nem működik egy ajtózár), Higiéniai probléma (pl. szennyezett a fürdőszoba)



Facilities
- id | Integer
- name | String
- equipments | List<Equipment> | DBben az equipment oldalrol van relacio




Statusz atmenet leiras

Issue

    Open, (Felhasznalo modosithatja szabadon)
    Blocked, (Eszkoz rendeles alatt, kiosztott feladat nem modosithato)
    InProgress, (Beszereles folyamatban)
    Fixed, (Beszereles megtortent)
    Verified, (User response megtortent, innen mar csak closed lehet)
    Closed


Kapcsolatok
Egy a több kapcsolat az Issue és az Equipment között. (Egy problémához több eszköz is kapcsolódhat, de egy eszközöz egy probléma.)