import Vue from 'vue';

let APIConnectionURL;
let APISignalRHub;
let APIAuthenticationURL;

if (Vue.config.devtools)
{
    APIConnectionURL = '';
    APISignalRHub = '';
    APIAuthenticationURL = '';
}
else
{
    APIConnectionURL = '';
    APISignalRHub = '';
    APIAuthenticationURL = '';
}

export default { APIConnectionURL, APISignalRHub, APIAuthenticationURL };