class CreateInGameQuizzes < ActiveRecord::Migration
  def change
    create_table :in_game_quizzes do |t|
      t.string  :email
      t.integer :quiz
      t.string :question
      t.string :answer
      t.boolean :correct	

      t.timestamps null: false
    end
  end
end
