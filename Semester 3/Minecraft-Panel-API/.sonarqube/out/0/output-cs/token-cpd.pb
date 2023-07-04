á
Ž/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api.Players.DAL/Context/MinecraftPluginContext.cs
	namespace 	
minecraft_panel_api
 
. 
Players %
.% &
DAL& )
.) *
Context* 1
{ 
public 

class "
MinecraftPluginContext '
:( )
	DbContext* 3
{ 
public "
MinecraftPluginContext %
(% &
DbContextOptions& 6
<6 7"
MinecraftPluginContext7 M
>M N
optionsO V
)V W
:X Y
baseZ ^
(^ _
options_ f
)f g
{h i
}j k
public

 
DbSet

 
<

 
Player

 
>

 
Players

 $
{

% &
get

' *
;

* +
set

, /
;

/ 0
}

1 2
} 
} Ü
‘/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api.Players.DAL/Migrations/20201103212917_Initial.cs
	namespace 	
minecraft_panel_api
 
. 
Players %
.% &
DAL& )
.) *

Migrations* 4
{ 
public 

partial 
class 
Initial  
:! "
	Migration# ,
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{		 	
migrationBuilder

 
.

 
CreateTable

 (
(

( )
name 
: 
$str 
,  
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
nullable, 4
:4 5
false6 ;
); <
,< =
UserName 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
nullable4 <
:< =
true> B
)B C
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 1
,1 2
x3 4
=>5 7
x8 9
.9 :
Id: <
)< =
;= >
} 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
	DropTable &
(& '
name 
: 
$str 
)  
;  !
} 	
} 
} ¿
}/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api.Players.DAL/Models/Player.cs
	namespace 	
minecraft_panel_api
 
. 
Players %
.% &
DAL& )
.) *
Models* 0
{ 
public 

class 
Player 
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
UserName 
{  
get! $
;$ %
set& )
;) *
}+ ,
}		 
}

 