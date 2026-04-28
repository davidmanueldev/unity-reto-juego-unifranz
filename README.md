# Reto: "Juego" - Gráfica y Multimedia 2

Este repositorio contiene el desarrollo del proyecto final del "Reto: Juego", construido en Unity (Universal Render Pipeline - URP). 

El proyecto se está desarrollando por etapas, aplicando principios de Programación Orientada a Objetos (POO), máquinas de estados, sistemas de cámara dinámicos y control de versiones con Git.

## 🎮 Controles del Jugador (Etapa 1 & 2)

*   **W, A, S, D / Flechas:** Mover al personaje.
*   **Ratón (Mover):** Controlar la cámara orbital (Cinemachine FreeLook).
*   **Barra Espaciadora:** Saltar.
*   **Click Izquierdo (Próximamente):** Disparar arma (Raycast).

## 📂 Arquitectura y Jerarquía de Scripts

Para mantener un código limpio y escalable, el proyecto divide sus responsabilidades en diferentes scripts:

### 1. Locomoción y Estado (`PlayerController.cs`)
Ubicado en el GameObject principal `Player`.
*   **Componentes requeridos:** `CharacterController`.
*   **Responsabilidad:** Lee los inputs de movimiento del teclado y maneja la gravedad. Gira al personaje fluidamente basándose en la dirección hacia la que mira la cámara principal.
*   **Lógica:** Implementa una Máquina de Estados finita (FSM) básica mediante un `enum PlayerState { Idle, Run, Jump }` para controlar las transiciones de comportamiento.

*(Se irán documentando más scripts a medida que avancemos en las Etapas 2, 3 y 4, como el Sistema de Combate, IA y UI).*

## 🛠️ Tecnologías Utilizadas

*   **Motor:** Unity 6000.x (URP)
*   **Cámara:** Unity Cinemachine
*   **Control de Versiones:** Git / GitHub
*   **Modelos y Animaciones:** Mixamo FBX Assets
