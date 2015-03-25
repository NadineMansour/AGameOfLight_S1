class CreateRecords < ActiveRecord::Migration

  #external documentation mariam
  #The table record keeps track of each level the student plays
  #Student is identifies by its email instead of id
  #we store the number of clicks , time spent and score for each level 
  #end mariam



  def change
    create_table :records do |t|
      t.string :email
      t.integer :level
      t.integer :time
      t.integer :score
      t.integer :clicks

      t.timestamps null: false
    end
  end
end
