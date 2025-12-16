# 🔎 Procedure Blackout 💉🩸
## 🕯️ Introduction 
A first-person psychological horror **survival investigation** game set in an abandoned psychiatric hospital.

The player must explore a hostile and mysterious environment while managing survival conditions such as **hydration and sanity**, using limited tools and resources to stay functional. As the investigation progresses, the player finds clues/reads notes, and uncovers the truth behind a series of disturbing events hidden within the hospital.

![Image](https://github.com/user-attachments/assets/7b2c1a34-ea60-4ce3-a06e-81f3d9e26dfa)

## 🔦 Win/Lose conditions

#### <ins> Win Conditions </ins>

The player wins by correctly answering all questions in the final report.
In Procedure Blackout, the player takes on the role of a journalist or detective exploring an abandoned psychiatric hospital. By collecting documents, audio recordings, and environmental clues throughout the hospital, the player gradually uncovers the hidden truth. The final report challenges the player to piece together all the gathered information and draw the correct conclusions.

#### <ins> Lose Conditions </ins>🩸

The player loses when the hydration bar reaches zero.
Instead of a traditional death mechanic, the game uses a more grounded and realistic outcome: the player becomes dehydrated and is forced to return home to rest. This design choice reinforces the psychological tone of the game, emphasizing vulnerability and limitation rather than punishment, while allowing the player to return later to continue uncovering the hospital’s secrets.

## 🔎 Set Up 
1. Open the project using **_Unity 2022.3.50f1 (LTS)_**
2. Open the main scene:
   ```
   Assets/Scenes/SampleScene.unity
   ```
4. Press **Play** in the Unity Editor to start the game.
5. Make sure your audio is enabled (headphone recommended for full experience).

## 🩸 Features 

#### 👁️‍🗨️ **<ins>_First-Person Exploration_</ins>**
- Free movement and camera control in a 3D environment
- Immersive abandoned psychiatric hospital setting

The game is experienced from a first-person perspective, allowing the player to move freely and control the camera in a fully 3D environment. The abandoned psychiatric hospital is designed to feel enclosed and unsettling, with long corridors, forgotten rooms, and dim lighting that encourage slow, cautious exploration. Movement itself becomes part of the tension, as the player is never certain what they might encounter next.

![Image](https://github.com/user-attachments/assets/887a48a6-d1de-4e11-8a8d-3dde94cc0115)

#### 👁️‍🗨️ **<ins>_Narrative-Driven Gameplay_</ins>**
- Story revealed through written notes and voice-over logs
- First-person narration from medical staff on night shifts
- Gradual uncovering of truth vs hallucination

The story is not presented directly, but gradually revealed through written notes and voice-over logs scattered throughout the hospital. Many of these accounts come from medical staff working night shifts, capturing moments of fear, doubt, and confusion. As more pieces are discovered, the player begins to question whether the events described are supernatural in nature or the result of human actions and psychological breakdowns.

#### 👁️‍🗨️ **<ins>_Health & Status System (Hydration and Sanity)_</ins>**
The player’s condition is represented through status bars rather than a single traditional health system. **Hydration** reflects the player’s physical endurance over time, while **Sanity** represents mental stability within an isolating and unsettling environment. When the hydration bar reaches zero, the game ends, signifying that the player was unable to endure long enough to uncover the secrets of the abandoned psychiatric hospital.

As sanity decreases, the player may begin to experience audio hallucinations, creating uncertainty about what is real and what is imagined. Additional hallucination effects and visual distortions are planned for future development. Items found throughout the hospital can temporarily restore these states, but resources are limited. Managing hydration and sanity becomes an ongoing concern, reinforcing the idea that survival is not only about staying alive, but remaining mentally and physically capable of continuing the investigation.

#### 👁️‍🗨️ **<ins>_Interactive Notes System_</ins>**
- Readable notes placed throughout the map
- Optional voice-over audio for notes
- Smooth UI transitions with pause handling

Notes can be found and read throughout the map, each contributing a small but important fragment of the overall narrative. Some notes include optional voice-over narration, allowing the player to hear the emotional state behind the written words. Opening and closing notes transitions smoothly into and out of gameplay, maintaining immersion while temporarily shifting the player into a focused reading experience.

![Image](https://github.com/user-attachments/assets/dad9b0e7-a6f5-4f6f-8c4e-aca4571f885d)

#### 👁️‍🗨️ **<ins>_Audio-Based Atmosphere_</ins>**
- Environmental ambience and sound effects
- Voice-over narration for intro and notes
- Psychological horror sound design

Sound is used to quietly shape the player’s experience rather than overwhelm it. The hospital is filled with low, constant ambience-distant echoes, faint mechanical hums, and sounds that are difficult to place. Voice-over narration appears during the introduction and in selected notes, adding a human presence to otherwise empty spaces. The audio design focuses on psychological discomfort, creating unease through silence, subtle cues, and uncertainty instead of sudden or aggressive effects.

#### 👁️‍🗨️ **<ins>_Clue & Progression System_</ins>**
- Collect clues by reading notes
- Unlock a final report (quiz) after enough clues are found
- Wrong answers require further exploration

Progression is built around observation and interpretation. By reading notes and listening to recordings, the player slowly collects fragments of information scattered throughout the hospital. These clues do not explain themselves all at once, but instead accumulate over time, forming a clearer picture of what may have happened. Once enough evidence is gathered, a final report sequence becomes available, asking the player to reflect on what they have learned. Incorrect conclusions do not immediately end the experience, but suggest that something is still missing and encourage further exploration.

![Image](https://github.com/user-attachments/assets/0941bc3e-ad4e-43af-a468-240b7b800848)

#### 👁️‍🗨️ **<ins>_Intro & Ending Panels_</ins>**
- Introductory voice sequence with skip option
- Win and Game Over panels with replay support

The game opens with a short introductory voice sequence that establishes mood rather than providing direct answers. Returning players are given the option to skip this sequence. The ending is determined by the player’s conclusions, leading to either a win or a game over panel. In both cases, the player is allowed to replay the game without restarting the project, reinforcing the investigative nature of the experience.

![Image](https://github.com/user-attachments/assets/2421f89f-ad03-483c-9149-d3ef528ae924)

#### 👁️‍🗨️ **<ins>_UI & Game Flow Management_</ins>**
- Central GameManager handling UI states
- Cursor and time control for seamless transitions
- Replay functionality without restarting the editor

## 🕹️ Controls

- Use **W / A / S / D** for movements
- Use **Mouse** to look around
- Press **R** to read note (when prompted)
- Press **C** to close note
