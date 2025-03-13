/* ****************************************************************************************
*                                       Locals
**************************************************************************************** */
let estudianteId = 0;

/* ****************************************************************************************
*                                    API Requests
**************************************************************************************** */
getEstudiantes = async () => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		}
	};
	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/estudiantes`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}

getEstudiante = async (id) => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		}
	};
	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/estudiantes/${id}`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}

/* ****************************************************************************************
*                                    JS Functions
**************************************************************************************** */
verEstudiante = async () => {
	estudianteId = window.location.search.split('estudianteId=')[1];

	if (estudianteId == 0) {
		$('#nombreEstudiante').html('John Doe');
		$('#correoEstudiante').html('somemail@example.com');
	}
	else {
		let datos = (await getEstudiante(estudianteId)).Value;
		$('#nombreEstudiante').html(datos.Nombre);
		$('#correoEstudiante').html(datos.Correo);
	}
}

verTareas = () => {
	window.location.href = 'tareas.html?estudianteId=' + estudianteId;
}