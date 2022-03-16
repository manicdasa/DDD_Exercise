var url = '/Payment/GetPaypalCredentials'

function getData(url, cb) {
    fetch(url)
      .then(response => response.json())
      .then(result => cb(result));
}
  
getData(url, (data) =>
{ 
    var doc = document.getElementById('paypal-container');
    if(doc != null){
    var sandbox = '';
    if(data.sandbox === true) { sandbox = 'sandbox' }
    (paypal.PayoutsAAC.render({
        env: sandbox,
        clientId: { sandbox: data.paypalClientID },
        merchantId: data.merchantId,
        pageType: "signup",
        onLogin: function(response) 
        {
            if (response.err) 
            {
            } 
            else 
            {
                window.CODE = response.body.code;
            }
        }
    }, '#paypal-container'))}
})
