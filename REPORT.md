# Report

During the development of *Procedure Blackout*, we encountered several technical and design challenges. This report highlights the most significant issues and the fixes applied.

### **UI State Conflicts (Cursor & Time Control)**

One of the main challenges was managing multiple UI panels (notes, quiz, wrong answer, return home, win screen) without causing conflicts in cursor state and player movement. Initially, opening and closing different panels caused issues where the cursor would become stuck, player movement would lock unexpectedly, or UI elements would become unclickable.

#### **<ins>_Fix:_</ins>**
We centralized UI state handling through the GameManager, ensuring that cursor visibility and Time.timeScale are consistently managed when entering and exiting UI modes. Panels now clearly define when the game should pause or resume, preventing overlapping UI states.
 
### **Quiz Flow and Wrong Answer Handling**

When the player answered a quiz question incorrectly, the wrong answer panel sometimes triggered unintended UI behavior, such as reopening the intro panel or preventing the quiz from appearing again after collecting new clues.

#### **<ins>_Fix:_</ins>**
We revised the quiz flow logic so that wrong answers no longer trigger unrelated UI panels. Progress flags were cleaned up to ensure that quizzes are only unlocked when the required number of clues is collected, and that retry logic does not interfere with normal gameplay progression.

### **Player Movement Lock After UI Interactions**

After reading notes or closing UI panels, the player occasionally lost movement control while the mouse remained active. This issue was especially noticeable after closing notes or wrong answer panels.

#### **<ins>_Fix:_</ins>**
We carefully audited where Time.timeScale, cursor lock state, and player input were modified. By ensuring that gameplay control is restored only after all UI panels are fully closed, player movement and camera control now resume correctly.

### **Health & Status Reset on Restart**

When the player reached game over conditions (such as hydration reaching zero), restarting the game did not reset hydration and sanity values, causing the player to immediately fail again.

#### **<ins>_Fix:_</ins>**
Instead of reloading the scene, we implemented a controlled reset through the GameManager. Player status values (hydration, sanity, flashlight battery) are now explicitly reset when returning to the start menu, ensuring a clean restart without relying on scene reloads.


### **Narrative Delivery Without Overexposure**

Another challenge was conveying a complex narrative without overwhelming the player or relying on excessive exposition. The story needed to feel fragmented, uncertain, and investigative rather than directly explained.

#### **<ins>_Fix:_</ins>**
The narrative is delivered through environmental storytelling, written notes, and optional voice logs. This allows players to piece together the truth at their own pace, reinforcing the theme of uncertainty and psychological tension.
