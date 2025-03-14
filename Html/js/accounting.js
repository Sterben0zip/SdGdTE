/* ****************************************************************************************
*                                       Locals
**************************************************************************************** */

/* ****************************************************************************************
*                                    API Requests
**************************************************************************************** */
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
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/login`, settings);
		const result = await fetchResponse.json();
		return result;
	} catch (e) {
		return e;
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
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/registro`, settings);
		const result = await fetchResponse.json();
		return result;
	} catch (e) {
		return e;
	}
};

/* ****************************************************************************************
*                                    JS Functions
**************************************************************************************** */

iniciarSesion = async () => {
	$("#btnIniciarSesion").prop('disabled', true);
	$("#spinn").attr('hidden', false);

	let datos = {
		correo: $('#correo').val(),
		password: $('#password').val()
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

	$("#btnIniciarSesion").prop('disabled', false);
	$("#spinn").attr('hidden', true);
};


registro = async ()=> {
	$("#btnRegistrar").prop('disabled', true);

	let datos = {
		correo: $('#correo').val(),
		password: $('#password').val(),
		nombre: $('#nombre').val()
	};
	
	let registrado = (await registrar(datos)).Value;
	if(registrado){
		alert('Usuario registrado con éxito');
		window.location.href = 'login.html?correo=' + datos.correo;
	}
	else {
		alert('Error al registrar');
	}

	$("#btnRegistrar").prop('disabled', false);
}