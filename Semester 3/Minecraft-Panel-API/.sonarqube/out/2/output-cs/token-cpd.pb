¤
ˆ/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api.GateWay/Controllers/PlayerController.cs
	namespace 	
minecraft_panel_api
 
. 
GateWay %
.% &
Controllers& 1
{		 
[

 
ApiController

 
]

 
[ 
Route 

(
 
$str 
) 
]  
public 

class 
PlayerController !
:" #
ControllerBase$ 2
{ 
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
string  
>  !
GetPlayerWithUser" 3
(3 4

InputModel4 >

inputModel? I
)I J
{ 	

RestClient 
baseUrl 
=  
new! $

RestClient% /
(/ 0
$str0 H
)H I
;I J
RestRequest #
playerDataClientRequest /
=0 1
new2 5
RestRequest6 A
(A B
$strB a
,a b
Methodc i
.i j
POSTj n
,n o

DataFormatp z
.z {
Json{ 
)	 €
;
€ 
RestRequest !
userDataClientRequest -
=. /
new0 3
RestRequest4 ?
(? @
$str@ a
,a b
Methodc i
.i j
POSTj n
,n o

DataFormatp z
.z {
Json{ 
)	 €
;
€ !
userDataClientRequest !
.! "
	AddHeader" +
(+ ,
$str, 4
,4 5
$str6 H
)H I
;I J#
playerDataClientRequest #
.# $
	AddHeader$ -
(- .
$str. 6
,6 7
$str8 J
)J K
;K L!
userDataClientRequest !
.! "
AddJsonBody" -
(- .
new. 1
{2 3

inputModel4 >
.> ?
Name? C
}D E
)E F
;F G#
playerDataClientRequest #
.# $
AddJsonBody$ /
(/ 0
new0 3
{4 5

inputModel6 @
.@ A
UserNameA I
}J K
)K L
;L M
var 

playerData 
= 
await "
baseUrl# *
.* +
	PostAsync+ 4
<4 5
dynamic5 <
>< =
(= >#
playerDataClientRequest> U
,U V
CancellationTokenW h
.h i
Nonei m
)m n
;n o
var 
userData 
= 
await  
baseUrl! (
.( )
	PostAsync) 2
<2 3
dynamic3 :
>: ;
(; <!
userDataClientRequest< Q
,Q R
CancellationTokenS d
.d e
Nonee i
)i j
;j k
return 
$str 
; 
} 	
}   
}!! §
}/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api.GateWay/Models/InputModel.cs
	namespace 	
minecraft_panel_api
 
. 
GateWay %
.% &
Models& ,
{ 
public 

class 

InputModel 
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
UserName 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ¢
s/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api.GateWay/Program.cs
	namespace 	
minecraft_panel_api
 
. 
GateWay %
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. $
ConfigureWebHostDefaults )
() *

webBuilder* 4
=>5 7
{ 

webBuilder 
. 

UseStartup )
<) *
Startup* 1
>1 2
(2 3
)3 4
;4 5
} 
) 
. %
ConfigureAppConfiguration *
(* +
(+ ,
hostingContext, :
,: ;
config< B
)B C
=>D F
{ 
config 
. 
SetBasePath $
($ %
hostingContext% 3
.3 4
HostingEnvironment4 F
.F G
ContentRootPathG V
)V W
. 
AddJsonFile $
($ %
$str% 8
,8 9
optional: B
:B C
falseD I
,I J
reloadOnChangeK Y
:Y Z
true[ _
)_ `
;` a
}   
)   
;   
}"" 
}## ß
s/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api.GateWay/Startup.cs
	namespace 	
minecraft_panel_api
 
. 
GateWay %
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{ 	
services 
. 
	AddOcelot 
( 
Configuration ,
), -
;- .
services 
. 
AddControllers #
(# $
)$ %
;% &
}   	
public## 
async## 
void## 
	Configure## #
(### $
IApplicationBuilder##$ 7
app##8 ;
,##; <
IWebHostEnvironment##= P
env##Q T
)##T U
{$$ 	
if%% 
(%% 
env%% 
.%% 
IsDevelopment%% !
(%%! "
)%%" #
)%%# $
{&& 
app'' 
.'' %
UseDeveloperExceptionPage'' -
(''- .
)''. /
;''/ 0
}(( 
app,, 
.,, 

UseRouting,, 
(,, 
),, 
;,, 
app.. 
... 
UseAuthorization..  
(..  !
)..! "
;.." #
app00 
.00 
UseEndpoints00 
(00 
	endpoints00 &
=>00' )
{00* +
	endpoints00, 5
.005 6
MapControllers006 D
(00D E
)00E F
;00F G
}00H I
)00I J
;00J K
await22 
app22 
.22 
	UseOcelot22 
(22  
)22  !
;22! "
}33 	
}44 
}55 