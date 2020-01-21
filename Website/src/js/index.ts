///dumb mistake:
///used a single typescript file due to laziness
///each page has a diffrent class for the bodies
///
///questions->mainBody
///graphs->mainBodyB
///index->mainBodyA
///
///no hate

import axios,{
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"


    interface Conversation {
        question:string,
        id:number,
        timeOfConversation:string
    }
///Conversation list
let list:Conversation[]=[];
///Conversation map, acts as a C# Dictionary (more or less)
///stores the frequency of a question
///e.g.
///console.log(map["ok google"])
///>3

///if we want to sort it by frequency we have to create yet another list where we sort it :/
///maybe create a second interface with a frequency inside, might complicate things
let map:{[id:string]:number;}={};

let i=1
///axios works just fine, we get the list of conversations and also create the map
        axios.get<Conversation[]>("https://conversationrestservice20191209113100.azurewebsites.net/api/conversations").then((response:AxiosResponse)=>{
        try{
        document.getElementById("contentA").innerHTML+='<ul id="questionList">';
        }
        catch{

        }
        response.data.forEach((element:Conversation) => {
            list[i]=element

            if (map[element.question]==null){
                map[element.question]=0;
            }
            map[element.question]+=1;
            
            
            i++
            
            try{
            document.getElementById("questionList").innerHTML+="<li>"+element.question+"</li>"
            }
            catch{

            }
        });


for (let key in map){
    try{
    if (map[key]>=2)
    document.getElementById("contentB").innerHTML+='<div class="val">'+key+"  "+'</div><div class="chart" style="width:'+map[key]*20+'px;">'+map[key]+'<br>';
    }
    catch{

    }
}

try{
document.getElementById("contentA").innerHTML+="</ul>";
}
catch{

}
})



function searchPress():void{

 
    console.log("here2");
   }
   
var button2:HTMLButtonElement=<HTMLButtonElement>(document.getElementById("searchButton"));

let div:HTMLDivElement=<HTMLDivElement>(document.getElementById("buttonDiv"));
let input:HTMLInputElement=<HTMLInputElement>(document.getElementById("searchBar"));
document.getElementById("searchButton").addEventListener("click",searchPress, false);
button2.addEventListener("click",function(){console.log("blabla")}, false);
div.addEventListener("click",searchPress);




