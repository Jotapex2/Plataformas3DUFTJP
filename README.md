# Waqaj, el guerrero climático


**Prototipo 3D plataformas para el Diplomado en Diseño y desarrollo de videojuegos 3d en UNITY de la Universidad Finis Terrae**
Por Juan Pablo González Urriola

# Juego publicado en Itch.io:
https://jpjuanpajp.itch.io/waqajelaventureroclimtico

# Estructura de niveles y lore


Waqaj: El aventurero climático, tiene que rescatar las tres estrellas de poder de cada uno de los planetas que están siendo destruídos por el cambio climático

**Nivel 1:
Mouri, un planeta tranquilo que está siendo devorado por la lava.

Mouri es una isla que está siendo destruido por la lava (por algo los agujeros en el terrain), es un nivel plataformero, con zonas para enemigos que faltan agregar, aunque, dado el diseño del nivel, no es necesario meter físicas muy complejas (aunque estoy revisando si se podrían insertar los joins como plataformas móviles). También ocupo unos lasers con raycast como obstáculo en la primera parte del nivel.

También ocupo unos lasers con raycast como obstáculo en la primera parte del nivel y hay plataformas “trampa” pintadas de rojo, son las mismas plataformas normales, pero con el collider desactivado, así el player cae a la lava.

**Nivel 2:
Destri es un nivel de agua, Este se compone de una serie de plataformas estáticas y las de cuerda (joint), donde el jugador tiene que ir avanzando, esquivando y/o matando a los enemigos, hasta llegar al centro de la isla del medio. Ahí el jugador caerá a una zona subterránea donde está la estrella de poder.

Este nivel tiene Rigidbody (Enemigo 1 y tres), máquinas de estado del enemigo 2 (torreta). Además de tener físicas de Joints con las cuerdas.

**Nivel 3:
Quaj, otro planeta devastado tras una guerra nuclear.
El jugador pasa por una planta de energía nuclear devastada por las bombas atómicas, es un nivel un poco más cortos deque los anteriores. Pero tiene como principal agregado el enemigo 3, que es un enemigo con una máquina de estado de disparo y tiene Rigibody con navmesh. A diferencia de la torreta de los niveles uno y dos, este puede ser asesinado cuando el jugador salta encima de él

# Cambios para la entrega final

Menú de pausa con sistema de guardado y cargado.

Final de juego y créditos.

Menú de inicio mejorado.

Enemigos 1 y 3 cuentan con trails. 

Agregadas partículas para las monedas y los enemigos.

Agregados efectos de sonido.

Los otros requisitos de la entrega final, fueron incluidos en entregas anteriores.

# Cambios para la entrega del 2 de diciembre

Enemigos:

Enemigo 1 (Nav Mesh)

![1](https://github.com/Jotapex2/Plataformas3DUFTJP/assets/144637089/53943368-eff7-44b3-8cfb-6aef63086984)

Este es un pequeño cubo que ocupa el Nav Mesh para moverse y acosar al jugador hasta matarlo, el jugador puede matarlo saltando encima de él, como un goobma del super mario. Al hacerlo, el enemigo suelta una moneda como recompensa.

Enemigo 2 (FSM)

![2](https://github.com/Jotapex2/Plataformas3DUFTJP/assets/144637089/05c9dfd1-2f0b-406f-b21c-ae99f5f60309)

Esta es una torreta triángulo que hasta el momento es inmortal, esta la compone tres estados: El estado entry que es cuando spamea en el nivel, el estado Vigilante y el estado ataque del estado entry al vigilante, la torreta pasa inmediatamente al spamear, 
el estado vigilante es cuando la torreta está en modo de espera a que el jugador se le acerque a su zona de ataque. Cuando el jugador se acerca a la zona de ataque, la torreta le dispara balas para quitarle vida, cuando el jugador sale de dicha zona, la torreta vuelve a su estado de vigilante.


Actualmente hay un cuarto estado “DIE”, ya que el plan es que la torreta también muera aplastada por el salto del jugador, pero esto quedó como una propiedad para el futuro. 

Enemigo 3 (FSM Y NavMesh)

El enemigo 3 es una fusión del enemigo 1 y 2, hay uno escondido en el primer nivel como testing, pero lo ideal es que sea solo del tercer nivel. 

![3](https://github.com/Jotapex2/Plataformas3DUFTJP/assets/144637089/defe1814-a80c-411c-8082-759931b139a7)

# Cambios para la entrega del 25 de noviembre


-Creación del primer nivel, con objeto Rigibody y raycasting (lasers).


-Creación de menú simple para iniciar el juego.


-Creación del lore del prototipo (al menos), con video intro.


-Inicio de la producción de los niveles 2 y 3, aunque falta desarrollarlos.


-Configurado mapa de luces para la escena de intro y los primeros tres niveles.


-Agregadas nuevas texturas y modelos complementarios al juego.


-Se agregaron fuentes nuevas para la UI del juego.


# Falta:
-Enemigos, Creación de un sistema de cambio de nivel, creación de menú de pausa, creación de sistema de savegame, creación de los niveles 2 y 3 (aunque están bosquejados), mejoras en el gamefeel, música y sonidos de efecto adecuados, pantalla de gameover, pantalla de créditos, posible mejora en el sistema de animaciones del personaje principal. 



# Cambios para la entrega del 18 de noviembre

-Creado nivel de prueba


-Agregado personaje con script de movimiento y de cámara


-Agregado sistema de vida


-Agregado sistema de pruebas de daño y curación


-Agregado coleccionable simple moneda


-Agregado coleccionable estrella (Te hace ganar el nivel)


-Agregado coleccionable vida extra: Te hace ganar una vida extra si es que no tienes el número máximo de vidas (5)


-Agregada las mecánicas de: Sprint, Doble salto y movimiento de plataforma, con dos códigos distintos.


-Pequeñas mejoras en las animaciones Idle y de correr.



