login = async (datos) => {
	const settings = {
		method: 'POST',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(datos),
	};

	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/login`, settings);
		const result = await fetchResponse.json();
		return result;
	} catch (e) {
		return e;
	}
};

iniciarSesion = async () => {
	let datos = {
		correo: $('#correo').val(),
		password: encryptString($('#password').val())
	};

	let result = await login(datos);

	if(result.StatusCode == 404) {
		alert('Error al iniciar sesión. ' + result.Value);
		console.log(result);
		
	}
	else {
		alert('Sesión iniciada con éxito');
		window.location.href = 'estudiantes.html?estudianteId=' + result.Value;
	}
};

registrar = async (datos) => {
	const settings = {
		method: 'POST',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(datos)
	};
	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/registro`, settings);
		const result = await fetchResponse.json();
		return result;
	} catch (e) {
		return e;
	}
};

registro = async ()=> {
	let datos = {
		correo: $('#correo').val(),
		password: encryptString($('#password').val()),
		nombre: $('#nombre').val()
	};
	
	let registrado = (await registrar(datos)).Value;
	if(registrado){
		alert('Usuario registrado con éxito');
	}
	else {
		alert('Error al registrar');
	}
}

encryptString = (message) => {
	const key = CryptoJS.enc.Utf8.parse('7f963202-24fc-44');
	const iv =  CryptoJS.enc.Utf8.parse('d989d54f-1ced-42');

	return CryptoJS.AES.encrypt(message, key, {
		iv: iv,
		mode: CryptoJS.mode.CBC,
		padding: CryptoJS.pad.ZeroPadding
	}).toString();
}